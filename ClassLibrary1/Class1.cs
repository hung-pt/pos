using System.ComponentModel;

namespace ClassLibrary1 {
    public class Class1 {
        public Class1() { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Age {
            get; set;
        }

        public int Height {
            get; set;
        }
    }
}
