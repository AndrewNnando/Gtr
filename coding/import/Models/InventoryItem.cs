using import.Interfaces;
using System.Text;

namespace import.Models
{
    internal class InventoryItem : IInventoryItem
    {
        public string[] Categories { get; set; } = new string[0];
        public string Name { get; set; }
        public string Twitter { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Name: \"{Name}\"; Categories: ");
            sb.Append($"{String.Join(',', Categories)}");
            if (!string.IsNullOrWhiteSpace(Twitter))
            {
                var tw = Twitter.First() == '@'
                    ? $"; Twitter: {Twitter}"
                    : $"; Twitter: @{Twitter}";
                sb.Append(tw);
            }
            return
                sb.ToString();
        }
    }
}
