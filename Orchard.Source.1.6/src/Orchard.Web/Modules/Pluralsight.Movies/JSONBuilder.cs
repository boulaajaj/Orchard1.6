using System.Text;

namespace Pluralsight.Movies {
    public class JSONBuilder {
        public StringBuilder _sb = new StringBuilder();
        private int _propCount = 0;
        private bool _openObject = false;

        public void AddNewObject() {
            _propCount = 0;
            if (_openObject)
                _sb.Append("},");
            _sb.Append("{");
            _openObject = true;
        }

        public void AddProperty(string propName, string propValue) {

            if (_propCount++ > 0)
                _sb.Append(",");

            _sb.Append(string.Format("{0}: '{1}'", propName, propValue));
            
        }
        public string Build() {
            if (!_openObject)
                return "no objects added to JSONBuilder!";

            return string.Format("[{0}}}]", _sb);
        }
    }
}