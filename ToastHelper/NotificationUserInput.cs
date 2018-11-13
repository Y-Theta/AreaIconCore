using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToastHelper {
   public class NotificationUserInput : IReadOnlyDictionary<string, string> {
        #region Properties
        private NotificationActivator.NOTIFICATION_USER_INPUT_DATA[] _data;

        internal NotificationUserInput(NotificationActivator.NOTIFICATION_USER_INPUT_DATA[] data) {
            _data = data;
        }

        public string this[string key] => _data.First(i => i.Key == key).Value;

        public IEnumerable<string> Keys => _data.Select(i => i.Key);

        public IEnumerable<string> Values => _data.Select(i => i.Value);

        public int Count => _data.Length;
        #endregion

        #region Methods
        public bool ContainsKey(string key) {
            return _data.Any(i => i.Key == key);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
            return _data.Select(i => new KeyValuePair<string, string>(i.Key, i.Value)).GetEnumerator();
        }

        public bool TryGetValue(string key, out string value) {
            foreach (var item in _data) {
                if (item.Key == key) {
                    value = item.Value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        #endregion

    }

}
