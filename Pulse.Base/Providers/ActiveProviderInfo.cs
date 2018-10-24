using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Pulse.Base
{
    public class ActiveProviderInfo : XmlSerializable<ActiveProviderInfo>
    {
        [Browsable(false)]
        public Guid ProviderInstanceID { get; set; }

        public bool Active { get; set; }

        [DisplayName("Label")]
        public string ProviderLabel { get; set; }

        [DisplayName("Name")]
        public string ProviderName { get; set; }

        [Browsable(false)]
        public int ExecutionOrder { get; set; }

        [Browsable(false)]
        public bool AsyncOK { get; set; }

        [Browsable(false)]
        public string ProviderConfig { get; set; }

        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public bool IsConfigurable { get { return ProviderManager.Instance.HasConfigurationWindow(ProviderName) != null; } }
 
        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public IProvider Instance { 
            get {
                if (_instance == null) _instance = ProviderManager.Instance.InitializeProvider(ProviderName, ProviderConfig);

                return _instance;
            } 
        }

        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public PictureList SearchResults
        {
            get {
                if (_searchResults != null)
                {
                    if (_searchResults.SearchSettingsHash != GetComparisonHashCode())
                        _searchResults = null;
                }

                return _searchResults;
            }
            set { _searchResults = value; }
        }

        private PictureList _searchResults = null;

        private IProvider _instance;

        public ActiveProviderInfo()
        {
            ProviderConfig = string.Empty;

            ProviderInstanceID = Guid.NewGuid();
        }

        public ActiveProviderInfo(string name, Guid id)
            : this(name)
        {
            ProviderInstanceID = id;
        }

        public ActiveProviderInfo(string name) : this()
        {
            ProviderName = name;
            ProviderLabel = name;
        }

        public int GetComparisonHashCode()
        {
            return ProviderConfig.GetHashCode() * 31 + Active.GetHashCode();
        }
    }
}
