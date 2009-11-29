using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using System.Reflection;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Cfg;

namespace Simple.Validation
{
    public class ValidatorEngineFactory : AggregateFactory<ValidatorEngineFactory>
    {
        ValidatorEngine _validator = null;

        public ValidatorEngine Validator
        {
            get
            {
                if (_validator == null)
                    return new ValidatorEngine();

                return _validator;
            }
        }


        public void Initialize(params Assembly[] assemblies)
        {
            var _fluent = new FluentConfiguration();
            _validator = new ValidatorEngine();

            foreach (var asm in assemblies)
                _fluent.Register(asm.ValidationDefinitions());

            _fluent.IntegrateWithNHibernate.ApplyingDDLConstraints().And.RegisteringListeners();
            _fluent.SetDefaultValidatorMode(ValidatorMode.OverrideExternalWithAttribute);
            _validator.Configure(_fluent);
        }
    }
}
