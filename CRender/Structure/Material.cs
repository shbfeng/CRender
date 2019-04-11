﻿using CRender.Pipeline;

namespace CRender.Structure
{
    public class Material : IAppliable<Material> 
    {
        public IShader Shader;

        public Material(IShader shader)
        {
            Shader = shader;
        }

        public Material GetInstanceToApply()
        {
            return this;
        }
    }
}