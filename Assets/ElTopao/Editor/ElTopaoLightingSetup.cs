using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace ElTopao.EditorTools
{
    /// <summary>
    /// Configura la iluminación HDRP del barrio: sol caribeño de las 4:30 PM (~100,000 lux),
    /// Physically Based Sky, niebla volumétrica, SSGI, exposición automática y ACES.
    /// </summary>
    public static class ElTopaoLightingSetup
    {
        const string SunName = "Sol_4_30PM";
        const string VolumeName = "Volumen Global - Barrio";
        const string SettingsFolder = "Assets/ElTopao/Settings";
        const string ProfilePath = SettingsFolder + "/BarrioVolumeProfile.asset";

        [MenuItem("El Topao/Iluminación/Configurar Sol 4:30 PM (HDRP)")]
        public static void Setup()
        {
            EnablePipelineFeatures();
            CreateSun();
            CreateGlobalVolume();
            Debug.Log("[El Topao] Iluminación HDRP configurada.");
        }

        /// <summary>SSGI, niebla volumétrica y decals deben estar habilitados en el HDRP Asset activo.</summary>
        static void EnablePipelineFeatures()
        {
            var asset = GraphicsSettings.currentRenderPipeline as HDRenderPipelineAsset;
            if (asset == null)
            {
                Debug.LogWarning("[El Topao] No hay un HDRenderPipelineAsset activo en Graphics Settings. " +
                                 "Usa la plantilla HDRP o asigna uno; SSGI/niebla volumétrica no se habilitaron.");
                return;
            }

            var settings = asset.currentPlatformRenderPipelineSettings;
            settings.supportSSGI = true;
            settings.supportVolumetrics = true;
            settings.supportDecals = true;
            asset.currentPlatformRenderPipelineSettings = settings;
            EditorUtility.SetDirty(asset);
        }

        static void CreateSun()
        {
            var go = GameObject.Find(SunName);
            if (go == null)
            {
                go = new GameObject(SunName);
                go.AddHDLight(LightType.Directional);
            }
            else if (go.GetComponent<HDAdditionalLightData>() == null)
            {
                go.AddHDLight(LightType.Directional);
            }

            var light = go.GetComponent<Light>();
            var hd = go.GetComponent<HDAdditionalLightData>();

            light.type = LightType.Directional;
            light.lightUnit = LightUnit.Lux;
            light.intensity = 100000f;
            light.useColorTemperature = true;
            light.colorTemperature = 5000f;               // cálido, de tarde
            light.color = new Color(1f, 0.90f, 0.76f);
            hd.EnableColorTemperature(true);
            hd.EnableShadows(true);
            hd.angularDiameter = 0.53f;
            hd.interactsWithSky = true;
            hd.volumetricDimmer = 1f;                      // haces de luz con el polvo del aire
            hd.useContactShadow.@override = true;

            // 4:30 PM: elevación baja (~30°) y azimut hacia el oeste, a contraluz de la calle.
            go.transform.rotation = Quaternion.Euler(30f, 250f, 0f);
            RenderSettings.sun = light;
        }

        static void CreateGlobalVolume()
        {
            var profile = LoadOrCreateProfile();

            var sky = Get<VisualEnvironment>(profile);
            sky.skyType.Override((int)SkyType.PhysicallyBased);
            sky.skyAmbientMode.Override(SkyAmbientMode.Dynamic);

            var pbs = Get<PhysicallyBasedSky>(profile);
            pbs.aerosolDensity.Override(0.35f);            // atmósfera polvorienta
            pbs.aerosolTint.Override(new Color(0.95f, 0.85f, 0.70f));

            var fog = Get<Fog>(profile);
            fog.enabled.Override(true);
            fog.enableVolumetricFog.Override(true);
            fog.meanFreePath.Override(90f);
            fog.baseHeight.Override(0f);
            fog.maximumHeight.Override(30f);
            fog.maxFogDistance.Override(500f);
            fog.albedo.Override(new Color(1f, 0.93f, 0.80f));
            fog.anisotropy.Override(0.65f);                // dispersión hacia adelante: contraluz

            var ssgi = Get<GlobalIllumination>(profile);
            ssgi.enable.Override(true);
            ssgi.fullResolutionSS.Override(true);

            var ssao = Get<ScreenSpaceAmbientOcclusion>(profile);
            ssao.intensity.Override(0.8f);
            ssao.radius.Override(1.2f);

            var contact = Get<ContactShadows>(profile);
            contact.enable.Override(true);
            contact.length.Override(0.2f);

            var exposure = Get<Exposure>(profile);
            exposure.mode.Override(ExposureMode.Automatic);
            exposure.limitMin.Override(7f);                // callejones oscuros
            exposure.limitMax.Override(15f);               // calle a pleno sol
            exposure.adaptationSpeedDarkToLight.Override(4f);
            exposure.adaptationSpeedLightToDark.Override(1.5f);

            var tone = Get<Tonemapping>(profile);
            tone.mode.Override(TonemappingMode.ACES);

            EditorUtility.SetDirty(profile);
            AssetDatabase.SaveAssets();

            var go = GameObject.Find(VolumeName);
            if (go == null) go = new GameObject(VolumeName);
            var volume = go.GetComponent<Volume>();
            if (volume == null) volume = go.AddComponent<Volume>();
            volume.isGlobal = true;
            volume.sharedProfile = profile;
        }

        static VolumeProfile LoadOrCreateProfile()
        {
            if (!Directory.Exists(SettingsFolder)) Directory.CreateDirectory(SettingsFolder);
            var profile = AssetDatabase.LoadAssetAtPath<VolumeProfile>(ProfilePath);
            if (profile != null) return profile;

            profile = ScriptableObject.CreateInstance<VolumeProfile>();
            AssetDatabase.CreateAsset(profile, ProfilePath);
            return profile;
        }

        static T Get<T>(VolumeProfile profile) where T : VolumeComponent
        {
            if (profile.TryGet(out T component)) return component;
            component = profile.Add<T>(true);
            AssetDatabase.AddObjectToAsset(component, profile);
            return component;
        }
    }
}
