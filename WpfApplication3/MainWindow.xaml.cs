using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ScriptSuite _scriptSuite;

        public MainWindow()
        {
            InitializeComponent();

            _scriptSuite = new ScriptSuite() { };

            XmlSerializer xmldes = new XmlSerializer(typeof(ScriptSuite));

            //string path = "1121212.xml";

            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    _scriptSuite = (ScriptSuite)xmldes.Deserialize(stream);
            //}

            TextWriter textWriter = new StreamWriter("mydata2.xml");

            xmldes.Serialize(textWriter, _scriptSuite);

            Console.WriteLine("OK");
        }
    }

    [Serializable]
    public class ScriptUnit
    {
        public ScriptUnit()
        {
            _scriptId = "04015";

            _cdataContext = "sdfhjsdlfjsdfkl<><><><>";
        }

        [XmlAttribute("Id")]
        public string _scriptId { get; set; }

        [XmlAttribute("Name")]
        public string _Name { get; set; }

        [XmlAttribute("IsActive")]
        public bool _isActive { get; set; }

        [XmlAttribute("IsApplicable")]
        public bool _isApplicable { get; set; }

        [XmlAttribute("Type")]
        public int _type { get; set; }

        [XmlAttribute("SequenceNumber")]
        public int _sequenceNumber { get; set; }

        [XmlAttribute("Version")]
        public string _version { get; set; }

        [XmlIgnore]
        public string _cdataContext { get; set; }

        public XmlNode Context
        {
            get
            {
                XmlNode node = new XmlDocument().CreateNode(XmlNodeType.CDATA, "", "");

                node.InnerText = _cdataContext;

                return node;
            }

            set
            {
                if (value == null)
                {
                    _cdataContext = null;
                }

                _cdataContext = value.Value;
            }
        }
    }

    [Serializable]
    public class ScriptGroup
    {
        public ScriptGroup()
        {
            _id = "abc";

            _name = "eg";

            _isActive = true;

            _isApplicable = false;

            _sequenceNumber = 101;

            _scrpits = new List<ScriptUnit>() { };

            _scrpits.Add(new ScriptUnit() { });

            _scrpits.Add(new ScriptUnit() { });

            _scrpits.Add(new ScriptUnit() { });
        }

        [XmlAttribute("Id")]
        public string _id { get; set; }

        [XmlAttribute("Name")]
        public string _name { get; set; }

        [XmlAttribute("IsActive")]
        public bool _isActive { get; set; }

        [XmlAttribute("IsApplicable")]
        public bool _isApplicable { get; set; }

        [XmlAttribute("SequenceNumber")]
        public int _sequenceNumber { get; set; }

        [XmlElement("SCRIPT")]
        public List<ScriptUnit> _scrpits { get; set; }
    }

    [Serializable]
    public class ScriptGroups
    {
        public ScriptGroups()
        {
            _scriptGroups = new List<ScriptGroup>();

            _scriptGroups.Add(new ScriptGroup() { });

            _scriptGroups.Add(new ScriptGroup() { });

            _scriptGroups.Add(new ScriptGroup() { });
        }

        [XmlElement("GROUP")]
        public List<ScriptGroup> _scriptGroups { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName ="TESTSUITE")]
    public class ScriptSuite
    {
        public ScriptSuite()
        {
            _name = "yanli";

            _scriptGroups = new List<ScriptGroups>();

            _scriptGroups.Add(new ScriptGroups());

            _scriptGroups.Add(new ScriptGroups());
        }

        [XmlAttribute("Name")]
        public string _name { get; set; }

        [XmlAttribute("Version")]
        public string _version { get; set; }

        [XmlAttribute("CSEdition")]
        public string _csEdition { get; set; }

        [XmlAttribute("CSMinimumVersion")]
        public string _csMinVersion { get; set; }

        [XmlElement("GROUPS")]
        public List<ScriptGroups> _scriptGroups; 
    }
}
