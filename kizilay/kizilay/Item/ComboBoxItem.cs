using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kizilay.Item
{
    public class ComboBoxItem
    {
        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
