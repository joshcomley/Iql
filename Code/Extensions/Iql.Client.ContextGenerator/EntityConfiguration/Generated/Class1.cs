namespace Iql.OData.TypeScript.Generator.EntityConfiguration
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Annotation
    {

        private AnnotationLabeledElement[] collectionField;

        private string termField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("LabeledElement", IsNullable = false)]
        public AnnotationLabeledElement[] Collection
        {
            get
            {
                return this.collectionField;
            }
            set
            {
                this.collectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Term
        {
            get
            {
                return this.termField;
            }
            set
            {
                this.termField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnnotationLabeledElement
    {

        private AnnotationLabeledElementLabeledElement[] collectionField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("LabeledElement", IsNullable = false)]
        public AnnotationLabeledElementLabeledElement[] Collection
        {
            get
            {
                return this.collectionField;
            }
            set
            {
                this.collectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnnotationLabeledElementLabeledElement
    {

        private AnnotationLabeledElementLabeledElementCollection collectionField;

        private string stringField;

        private string nameField;

        /// <remarks/>
        public AnnotationLabeledElementLabeledElementCollection Collection
        {
            get
            {
                return this.collectionField;
            }
            set
            {
                this.collectionField = value;
            }
        }

        /// <remarks/>
        public string String
        {
            get
            {
                return this.stringField;
            }
            set
            {
                this.stringField = value;
            }
        }

        private bool boolField;
        public bool Bool
        {
            get
            {
                return this.boolField;
            }
            set
            {
                this.boolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnnotationLabeledElementLabeledElementCollection
    {

        private AnnotationLabeledElementLabeledElementCollectionLabeledElement[] labeledElementField;

        private string[] stringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LabeledElement")]
        public AnnotationLabeledElementLabeledElementCollectionLabeledElement[] LabeledElement
        {
            get
            {
                return this.labeledElementField;
            }
            set
            {
                this.labeledElementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("String")]
        public string[] String
        {
            get
            {
                return this.stringField;
            }
            set
            {
                this.stringField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnnotationLabeledElementLabeledElementCollectionLabeledElement
    {

        private string stringField;

        private string nameField;
        private int numberField;

        /// <remarks/>
        public string String
        {
            get
            {
                return this.stringField;
            }
            set
            {
                this.stringField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Int")]
        public int Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }
    }

}