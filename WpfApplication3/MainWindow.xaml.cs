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

            _scriptSuite._scriptUnit._cdataContext = "<ABCDEFE>";

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
            _scriptId = "1254";

            _Name = "serialtest";

            _cdataContext = "include <string>";
        }

        [XmlAttribute("Id")]
        public string _scriptId { get; set; }

        [XmlElement("Name")]
        public string _Name { get; set; }

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
    [XmlRoot(ElementName ="TESTSUITE")]
    public class ScriptSuite
    {
        public ScriptSuite()
        {
            _name = "Yanli";

            _scriptUnit = new ScriptUnit();
        }

        [XmlAttribute("Name")]
        public string _name { get; set; }

        [XmlElement("SCRIPT")]
        public ScriptUnit _scriptUnit; 
    }
}
