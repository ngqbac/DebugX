using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace DebugX.Extensions
{
    public static class RendererExtensions
    {
        [PublicAPI]
        public static void AddMaterial(this Renderer meshRenderer, Material material)
        {
            var currentMats = meshRenderer.sharedMaterials;
            if (currentMats.Contains(material)) return;
            var newMats = new List<Material>(currentMats) { material };
            newMats[currentMats.Length] = material;
            meshRenderer.sharedMaterials = newMats.ToArray();
        }

        public static void ChangeMaterial(this Renderer meshRenderer, Material materialRoot, Material materialTarget)
        {
            var currentMats = meshRenderer.sharedMaterials;

            if (currentMats.Contains(materialRoot))
            {
                var index = System.Array.IndexOf(currentMats, materialRoot);
                var newMats = new List<Material>(currentMats)
                {
                    [index] = materialTarget
                };
                meshRenderer.sharedMaterials = newMats.ToArray();
            }
            else
            {
                AddMaterial(meshRenderer, materialTarget);
            }
        }

        public static void RemoveMaterial(this Renderer meshRenderer, Material material)
        {
            var currentMats = meshRenderer.sharedMaterials;
            if (!currentMats.Contains(material)) return;
            var index = System.Array.IndexOf(currentMats, material);
            var newMats = currentMats.Where((mat, i) => i != index).ToArray();
            meshRenderer.sharedMaterials = newMats;
        }
    }
}
