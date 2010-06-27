using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity.App;
using Simple.Patterns;

namespace Simple.NVelocity
{
    public class EngineWrapper : Singleton<EngineWrapper>
    {
        VelocityEngine _engine = null;
        public EngineWrapper()
        {
            _engine = new VelocityEngine();
            _engine.Init();
        }

        public VelocityEngine Get()
        {
            return _engine;
        }
    }
}
