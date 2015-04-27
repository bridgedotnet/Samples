using Bridge;
using Bridge.Html5;

// For more information please visit
// http://bridge.net/kb/bridge-qunit-sample-application/

namespace BridgeQUnitSample
{
    [FileName("../samples/qunit/output/app.js")]
    public class App
    {
        [Ready]
        public static void Main()
        {
            var personApplication = new PersonApplication(Document.Body);

            personApplication.RenderPerson();
        }
    }
}