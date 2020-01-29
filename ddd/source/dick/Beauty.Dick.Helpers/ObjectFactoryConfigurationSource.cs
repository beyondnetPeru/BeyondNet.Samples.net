using Beauty.Dick.Helpers.Builders.Interface.Impl;
using Beauty.Dick.Helpers.Builders.Model;
using Jal.Factory.Impl;

namespace Beauty.Dick.Helpers
{
    public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public ObjectFactoryConfigurationSource()
        {
            For<EnumCodeBuilder, SequencialFromDbCodeBuilder>().Create<SequencialFromDbCodeBuilder>().When(x => x == EnumCodeBuilder.Sequencial);
        }
    }
}
