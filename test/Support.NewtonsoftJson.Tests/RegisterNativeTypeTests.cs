using Newtonsoft.Json.Linq;

using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Support.NewtonsoftJson.Tests
{
    public class RegisterNativeTypeTests : VerifyBase
    {
        private readonly struct FancyId
        {
            public FancyId(string wrappedId)
            {
                WrappedId = wrappedId;
            }

            public string WrappedId { get; }
        }

        private readonly struct FancyFantomlyTypedId<TFantomInterface>
        {
            public FancyFantomlyTypedId(string wrappedId)
            {
                WrappedId = wrappedId;
            }

            public string WrappedId { get; }
        }

        interface FantomInterface { }

        public RegisterNativeTypeTests() : base()
        {
        }

        [Fact]
        public Task CanSerializeSimpleValueObject() => Verify(g
            .ConfigureEnvironment(env => env
                .UseNewtonsoftJson()
                .RegisterNativeType(
                    (fancyId, env, _, recurse) => fancyId.WrappedId,
                    (jValue, env, _, recurse) => new FancyId(jValue.Value<string>()!)))
            .Inject(new FancyId("fancyId"))
            .Debug());

        [Fact]
        public Task CanSerializeFantomlyTypedValueObject() => Verify(g
            .ConfigureEnvironment(env => env
                .UseNewtonsoftJson()
                .RegisterNativeType(
                    (fancyId, env, _, recurse) => fancyId.WrappedId,
                    (jValue, env, _, recurse) => new FancyFantomlyTypedId<FantomInterface>(jValue.Value<string>()!)))
            .Inject(new FancyFantomlyTypedId<FantomInterface>("fancyId"))
            .Debug());

        //TODO: DeserializationTests
    }
}
