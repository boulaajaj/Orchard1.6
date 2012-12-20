using System.Text;

namespace Pluralsight.Movies {
    public class JSONBuilder {
        public StringBuilder _sb = new StringBuilder();
        private int _propCount = 0;

        public void AddNewObject() {
            _sb.Append("{");
        }

        public void AddProperty(string propName, string propValue) {

            if (_propCount++ > 0)
                _sb.Append(",");

            _sb.Append(string.Format("{0}: '{1}'", propName, propValue));
            
        }
        public string Build() {
            return string.Format("[{0}}}]", _sb);
        }
    }
}