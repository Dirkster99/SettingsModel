namespace SettingsModelWPFDemo.Converters
{
    using System.Windows.Data;

    public class BulletListBinding : Binding
    {
        private string customStringFormat;

        public BulletListBinding()
        {
        }

        public string CustomStringFormat
        {
            get
            {
                return this.customStringFormat;
            }

            set
            {
                if (this.customStringFormat != value)
                {
                    this.customStringFormat = value;

                    if (!string.IsNullOrEmpty(this.customStringFormat))
                        this.StringFormat = string.Format("- ", this.customStringFormat);
                    else
                        this.StringFormat = "- ";
                }
            }
        }
    }
}
