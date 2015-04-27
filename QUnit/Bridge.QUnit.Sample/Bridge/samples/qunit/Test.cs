using Bridge;
using Bridge.Html5;
using Bridge.QUnit1;

namespace BridgeQUnitSample
{
    [FileName("../sample/qunit/output/test.js")]
    public class Test
    {
        [Ready]
        public static void RunTests()
        {
            QUnit.Module("PersonService");

            QUnit.Test("Method GetPerson()", (assert) =>
            {
                var service = Test.GetService();
                var person = service.GetPerson(55);

                assert.Expect(3);

                assert.Ok(person != null, "Person created");
                assert.DeepEqual(person.Id, 55, "person.Id = 55");
                assert.DeepEqual(person.Name, "Frank", "person.Name = 'Frank'");
            });

            QUnit.Module("PersonView");
            QUnit.Test("Method CreateLabelElement", Test.TestCreateLabel);

            QUnit.Module("PersonApplication");
            QUnit.Test("RenderPerson", Test.TestCreatePersonUIElements);
        }

        #region PersonView tests

        private static void TestCreateLabel(Assert assert)
        {
            assert.Expect(6);

            var view = Test.GetView();

            var label = view.CreateLabelElement("someLabel", "Title", "10px", true, HTMLColor.Blue);
            assert.Ok(label != null, "label created");

            view.Root.AppendChild(label);
            var foundLabel = Document.GetElementById<LabelElement>("someLabel");

            assert.Ok(foundLabel != null, "foundLabel");
            assert.DeepEqual(foundLabel.InnerHTML, "Title", "foundLabel.innerHtml = 'Title'");
            assert.DeepEqual(foundLabel.Style.Color, HTMLColor.Blue, "foundLabel.Style.Color = Blue");
            assert.DeepEqual(foundLabel.Style.Margin, "10px", "foundLabel.Style.Margin = '10px'");
            assert.DeepEqual(foundLabel.Style.FontWeight, "bold", "foundLabel.Style.FontWeight = 'bold'");
        }

        #endregion PersonView tests

        #region PersonApplication tests

        private static void TestCreatePersonUIElements(Assert assert)
        {
            assert.Expect(2);

            var application = Test.GetApplication();

            application.RenderPerson();

            var lblPersonName = Document.GetElementById<LabelElement>("lblPersonName");
            assert.Ok(lblPersonName != null, "lblPersonName created");
            assert.DeepEqual(lblPersonName.InnerHTML, "Frank", "lblPersonName = 'Frank'");
        }

        #endregion PersonApplication tests

        #region Helper methods

        private static PersonService GetService()
        {
            return new PersonService();
        }

        private static PersonView GetView()
        {
            return new PersonView(Test.EnsureTestFixture());
        }

        private static PersonApplication GetApplication()
        {
            return new PersonApplication(Test.EnsureTestFixture());
        }


        /// <summary>
        /// Ensures the div element with qunit-fixture exists.
        /// QUnit automatically cleans qunit-fixture div after each test. You should only rely on the fixture markup, inside the #qunit-fixture element.
        /// </summary>
        /// <returns></returns>
        private static DivElement EnsureTestFixture()
        {
            var fixture = Bridge.Html5.Document.GetElementById<DivElement>("qunit-fixture");

            if (fixture == null)
            {
                fixture = new DivElement()
                {
                    Id = "qunit-fixture"
                };

                Document.Body.AppendChild(fixture);
            }

            return fixture;
        }

        #endregion Helper methods
    }
}
