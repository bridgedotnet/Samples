using Bridge;
using Bridge.Html5;

namespace BridgeQUnitSample
{
    [FileName("../samples/qunit/output/code.js")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [FileName("../samples/qunit/output/code.js")]
    public class PersonService
    {
        public Person GetPerson(int personId)
        {
            return new Person
            {
                Id = personId,
                Name = "Frank"
            };
        }
    }

    [FileName("../samples/qunit/output/code.js")]
    public class PersonView
    {
        public Element Root { get; private set; }

        public PersonView(Element root)
        {
            this.Root = root;
        }

        public LabelElement CreateLabelElement(string id, string title, string margin, bool isBold, string color)
        {
            return new LabelElement()
            {
                Id = id,
                InnerHTML = title,
                Style =
                {
                    Margin = margin ?? string.Empty,
                    FontWeight = isBold ? "bold" : string.Empty,
                    Color = color
                }
            };
        }

        public void CreatePersonUIElements(Person person)
        {
            this.Root.AppendChild(this.CreateLabelElement(null, "Person:", "5px", true, HTMLColor.Black));

            this.Root.AppendChild(this.CreateLabelElement("lblPersonName", person.Name, null, false, HTMLColor.Blue));
        }
    }

    [FileName("../samples/qunit/output/code.js")]
    public class PersonApplication
    {
        public PersonService Service { get; private set; }
        public PersonView View { get; private set; }

        public PersonApplication(Element root)
        {
            this.Service = new PersonService();
            this.View = new PersonView(root);
        }

        public void RenderPerson()
        {
            var person = this.Service.GetPerson(55);

            this.View.CreatePersonUIElements(person);
        }
    }
}