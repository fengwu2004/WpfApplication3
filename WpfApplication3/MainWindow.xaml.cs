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

            //_scriptSuite = new ScriptSuite(){ };

            XmlSerializer xmldes = new XmlSerializer(typeof(ScriptSuite));

            string path = "mydata1.xml";

            using (var stream = new FileStream(path, FileMode.Open))
            {
                _scriptSuite = (ScriptSuite)xmldes.Deserialize(stream);
            }

            TextWriter textWriter = new StreamWriter("mydata2.xml");

            xmldes.Serialize(textWriter, _scriptSuite);

            Console.WriteLine("OK");
        }
    }

    [Serializable]
    public class ScriptUnit
    {
        [XmlAttribute("Id")]
        public string _scriptId { get; set; }

        [XmlElement("Name")]
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

        [XmlElement("Context")]
        public XmlNode Context
        {
            get
            {
                XmlNode node = new XmlDocument().CreateNode(XmlNodeType.CDATA, "","");

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
        [XmlAttribute("GROUP")]
        public List<ScriptGroup> _scriptGroups { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName ="TESTSUITE")]
    public class ScriptSuite
    {
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
