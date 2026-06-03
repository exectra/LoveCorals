namespace Yarn.Unity.Samples {

    using Yarn.Unity;

    [System.CodeDom.Compiler.GeneratedCode("YarnSpinner", "3.1.4.0")]
    public partial class YarnVariables : Yarn.Unity.InMemoryVariableStorage, Yarn.Unity.IGeneratedVariableStorage {
        // Accessor for Number $SWaffinity
        public float SWaffinity {
            get => this.GetValueOrDefault<float>("$SWaffinity");
            set => this.SetValue<float>("$SWaffinity", value);
        }

    }
}
