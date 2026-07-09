namespace Yarn.Unity.Samples {

    using Yarn.Unity;

    [System.CodeDom.Compiler.GeneratedCode("YarnSpinner", "3.1.4.0")]
    public partial class YarnVariables : Yarn.Unity.InMemoryVariableStorage, Yarn.Unity.IGeneratedVariableStorage {
        // Accessor for Number $SWCoralPoints
        public float SWCoralPoints {
            get => this.GetValueOrDefault<float>("$SWCoralPoints");
            set => this.SetValue<float>("$SWCoralPoints", value);
        }

        // Accessor for String $Speaker
        public string Speaker {
            get => this.GetValueOrDefault<string>("$Speaker");
            set => this.SetValue<string>("$Speaker", value);
        }

        // Accessor for Bool $CLBranch
        public bool CLBranch {
            get => this.GetValueOrDefault<bool>("$CLBranch");
            set => this.SetValue<bool>("$CLBranch", value);
        }

        // Accessor for Bool $GBBranch
        public bool GBBranch {
            get => this.GetValueOrDefault<bool>("$GBBranch");
            set => this.SetValue<bool>("$GBBranch", value);
        }

    }
}
