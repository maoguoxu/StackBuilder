﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;

using TreeDim.StackBuilder.Graphics;
using TreeDim.StackBuilder.Basics;
using TreeDim.StackBuilder.Engine;
#endregion

namespace TreeDim.StackBuilder.Desktop
{
    #region IDocumentListener
    public interface IDocumentListener
    {
        // new
        void OnNewDocument(Document doc);
        void OnNewTypeCreated(Document doc, ItemProperties itemProperties);
        void OnNewAnalysisCreated(Document doc, Analysis analysis);
        // remove
        void OnTypeRemoved(Document doc, Analysis analysis);
        void OnAnalysisRemoved(Document doc, Analysis analysis);
    }
    #endregion

    #region Document
    public class Document
    {
        #region Data members
        private string _name, _description, _author;
        private DateTime _dateCreated;
        private List<ItemProperties> _typeList = new List<ItemProperties>();
        private List<Analysis> _analyses = new List<Analysis>();
        private List<IDocumentListener> _listeners = new List<IDocumentListener>();
        private string _filePath = string.Empty;
        #endregion

        #region Constructor
        public Document(string filePath)
        {
            _filePath = filePath;
        }

        public Document(string name, string description, string author, DateTime dateCreated)
        {
            _name = name;
            _description = description;
            _author = author;
            _dateCreated = dateCreated;
        }
        #endregion

        #region Document Load method
        public static Document Load(string filePath, IDocumentListener docListener)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);            

            Document doc = new Document(filePath);
            doc.AddListener(docListener);



            return doc;
        }

        #endregion

        #region Public ItemProperties instantiation methods
        public BoxProperties CreateNewBox(
            string name, string description
            , double length, double width, double height, double weight
            , Color[] colors)
        {
            // instantiate and initialize
            BoxProperties boxProperties = new BoxProperties(length, width, height);
            boxProperties.Weight = weight;
            boxProperties.Name = name;
            boxProperties.Description = description;
            boxProperties.Colors = colors;
            // insert in list
            _typeList.Add(boxProperties);
            // notify listeners
            NotifyOnNewTypeCreated(boxProperties);

            return boxProperties;
        }

        public BundleProperties CreateNewBundle(
            string name, string description
            , double length, double width, double thickness
            , double weight
            , Color color
            , int noFlats)
        {
            // instantiate and initialize
            BundleProperties bundle = new BundleProperties(name, description, length, width, thickness, weight, noFlats, color);
            // insert in list
            _typeList.Add(bundle);
            // notify listeners
            NotifyOnNewTypeCreated(bundle);
            return bundle;
        }

        public InterlayerProperties CreateNewInterlayer(
            string name, string description
            , double length, double width, double thickness
            , double weight
            , Color color)
        { 
            // instantiate and intialize
            InterlayerProperties interlayer = new InterlayerProperties(
                name, description
                , length, width, thickness
                , weight, color);
            // insert in list
            _typeList.Add(interlayer);
            // notify listeners
            NotifyOnNewTypeCreated(interlayer);
            return interlayer;
        }

        public PalletProperties CreateNewPallet(
            string name, string description
            , PalletProperties.PalletType type
            , double length, double width, double height
            , double weight, double admissibleLoadWeight
            , double admissibleLoadHeight)
        {
            PalletProperties palletProperties = new PalletProperties(type, length, width, height);
            palletProperties.Name = name;
            palletProperties.Description = description;
            palletProperties.Weight = weight;
            palletProperties.AdmissibleLoadWeight = admissibleLoadWeight;
            palletProperties.AdmissibleLoadHeight = admissibleLoadHeight;
            // insert in list
            _typeList.Add(palletProperties);
            // notify listeners
            NotifyOnNewTypeCreated(palletProperties);

            return palletProperties;
        }

        public Analysis CreateNewAnalysis(string name, string description
            , BoxProperties box, PalletProperties pallet, InterlayerProperties interlayer
            , ConstraintSet constraintSet)
        {
            Analysis analysis = new Analysis(box, pallet, interlayer, constraintSet);
            analysis.Name = name;
            analysis.Description = description;
            // insert in list
            _analyses.Add(analysis);
            // notify listeners
            NotifyOnNewAnalysisCreated(analysis);

            // compute
            Solver solver = new Solver();
            solver.ProcessAnalysis(analysis);

            return analysis;
        }
        #endregion

        #region Name methods
        public bool IsValidTypeName(string name)
        { 
            // make sure is not empty
            if (name.Trim() == string.Empty)
                return false;            
            // make sure that name is not already used
            foreach (ItemProperties item in _typeList)
                if (item.Name.Trim().ToLower() == name.Trim().ToLower())
                    return false;            
            // success
            return true;
        }
        #endregion

        #region Public properties
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public List<BoxProperties> Boxes
        {
            get
            {
                List<BoxProperties> boxList = new List<BoxProperties>();
                foreach (ItemProperties item in _typeList)
                {
                    BoxProperties boxProperties = item as BoxProperties;
                    if (null != boxProperties)
                        boxList.Add(boxProperties);                        
                }
                return boxList;
            }
        }
        public List<PalletProperties> Pallets
        {
            get
            {
                List<PalletProperties> palletList = new List<PalletProperties>();
                foreach (ItemProperties item in _typeList)
                {
                    PalletProperties palletProperties = item as PalletProperties;
                    if (null != palletProperties)
                        palletList.Add(palletProperties);
                }
                return palletList;
            }
        }
        public List<InterlayerProperties> Interlayers
        {
            get
            {
                List<InterlayerProperties> interlayerList = new List<InterlayerProperties>();
                foreach (ItemProperties item in _typeList)
                {
                    InterlayerProperties interlayerProperties = item as InterlayerProperties;
                    if (null != interlayerProperties)
                        interlayerList.Add(interlayerProperties);
                }
                return interlayerList;
            }
        }

        public bool CanCreateAnalysis
        {
            get
            {
                int iNoBox = 0, iNoPallets = 0;
                foreach (ItemProperties item in _typeList)
                {
                    if (item.GetType() == typeof(BoxProperties))
                        ++iNoBox;
                    else if (item.GetType() == typeof(PalletProperties))
                        ++iNoPallets;
                }
                return iNoBox > 0 && iNoPallets > 0; 
            }
        }
        #endregion

        #region Save / load methods
        public void Save()
        {
            // get a valid file path
            if ( null == _filePath || string.Empty == _filePath || !File.Exists(_filePath) )
            {
                SaveFileDialog form = new System.Windows.Forms.SaveFileDialog();
                form.FileName = _name + ".stb";
                form.Filter = "StackBuilder files (*.stb)|*.stb|All files (*.*)|*.*";
                if (DialogResult.OK == form.ShowDialog())
                    _filePath = form.FileName;
                else
                    return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                // let's add the XML declaration section
                XmlNode xmlnode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmlDoc.AppendChild(xmlnode);
                // create Document (root) element
                XmlElement xmlRootElement = xmlDoc.CreateElement("Document");
                xmlDoc.AppendChild(xmlRootElement);
                // name
                XmlAttribute xmlDocNameAttribute = xmlDoc.CreateAttribute("Name");
                xmlDocNameAttribute.Value = _name;
                xmlRootElement.Attributes.Append(xmlDocNameAttribute);
                // description
                XmlAttribute xmlDocDescAttribute = xmlDoc.CreateAttribute("Description");
                xmlDocDescAttribute.Value = _description;
                xmlRootElement.Attributes.Append(xmlDocDescAttribute);
                // author
                XmlAttribute xmlDocAuthorAttribute = xmlDoc.CreateAttribute("Author");
                xmlDocAuthorAttribute.Value = _author;
                xmlRootElement.Attributes.Append(xmlDocAuthorAttribute);
                // dateCreated
                XmlAttribute xmlDateCreatedAttribute = xmlDoc.CreateAttribute("DateCreated");
                xmlDateCreatedAttribute.Value = string.Format("{0}", _dateCreated);
                xmlRootElement.Attributes.Append(xmlDateCreatedAttribute);

                // create ItemProperties element
                XmlElement xmlItemPropertiesElt = xmlDoc.CreateElement("ItemProperties");
                xmlRootElement.AppendChild(xmlItemPropertiesElt);
                foreach (ItemProperties itemProperties in _typeList)
                {
                    BoxProperties boxProperties = itemProperties as BoxProperties;
                    if (null != boxProperties)
                        Save(boxProperties, xmlItemPropertiesElt, xmlDoc);
                    PalletProperties palletProperties = itemProperties as PalletProperties;
                    if (null != palletProperties)
                        Save(palletProperties, xmlItemPropertiesElt, xmlDoc);
                    InterlayerProperties interlayerProperties = itemProperties as InterlayerProperties;
                    if (null != interlayerProperties)
                        Save(interlayerProperties, xmlItemPropertiesElt, xmlDoc);
                    BundleProperties bundleProperties = itemProperties as BundleProperties;
                    if (null != bundleProperties)
                        Save(bundleProperties, xmlItemPropertiesElt, xmlDoc);
                    TruckProperties truckProperties = itemProperties as TruckProperties;
                    if (null != truckProperties)
                        Save(truckProperties, xmlItemPropertiesElt, xmlDoc);
                }

                // create Analyses element
                XmlElement xmlAnalysesElt = xmlDoc.CreateElement("Analyses");
                xmlRootElement.AppendChild(xmlAnalysesElt);
                foreach (Analysis analysis in _analyses)
                {
                    Save(analysis, xmlAnalysesElt, xmlDoc);            
                }
                xmlDoc.Save(_filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Save(BoxProperties boxProperties, XmlElement parentElement, XmlDocument xmlDoc)
        {
            // create xmlBoxProperties element
            XmlElement xmlBoxProperties = xmlDoc.CreateElement("BoxProperties");
            parentElement.AppendChild(xmlBoxProperties);
            // Id
            XmlAttribute guidAttribute = xmlDoc.CreateAttribute("Id");
            guidAttribute.Value = boxProperties.Guid.ToString();
            xmlBoxProperties.Attributes.Append(guidAttribute);
            // name
            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Name");
            nameAttribute.Value = boxProperties.Name;
            xmlBoxProperties.Attributes.Append(nameAttribute);
            // description
            XmlAttribute descAttribute = xmlDoc.CreateAttribute("Description");
            descAttribute.Value = boxProperties.Description;
            xmlBoxProperties.Attributes.Append(descAttribute);
            // length
            XmlAttribute lengthAttribute = xmlDoc.CreateAttribute("Length");
            lengthAttribute.Value = string.Format("{0}", boxProperties.Length);
            xmlBoxProperties.Attributes.Append(lengthAttribute);
            // width
            XmlAttribute widthAttribute = xmlDoc.CreateAttribute("Width");
            widthAttribute.Value = string.Format("{0}", boxProperties.Width);
            xmlBoxProperties.Attributes.Append(widthAttribute);
            // height
            XmlAttribute heightAttribute = xmlDoc.CreateAttribute("Height");
            heightAttribute.Value = string.Format("{0}", boxProperties.Height);
            xmlBoxProperties.Attributes.Append(heightAttribute);
            // weight
            XmlAttribute weightAttribute = xmlDoc.CreateAttribute("Weight");
            weightAttribute.Value = string.Format("{0}", boxProperties.Weight);
            xmlBoxProperties.Attributes.Append(weightAttribute);
            // face colors
            XmlElement xmlFaceColors = xmlDoc.CreateElement("FaceColors");
            xmlBoxProperties.AppendChild(xmlFaceColors);
            short i = 0;
            foreach (Color color in boxProperties.Colors)
            {
                XmlElement xmlFaceColor = xmlDoc.CreateElement("FaceColor");
                xmlFaceColors.AppendChild(xmlFaceColor);
                // face index
                XmlAttribute xmlFaceIndex = xmlDoc.CreateAttribute("FaceIndex");
                xmlFaceIndex.Value = string.Format("{0}", i);
                xmlFaceColor.Attributes.Append(xmlFaceIndex);
                // color
                XmlAttribute xmlColor = xmlDoc.CreateAttribute("Color");
                xmlColor.Value = string.Format("{0}", color.ToArgb());
                xmlFaceColor.Attributes.Append(xmlColor);
                ++i;
            }
            // textures
            XmlElement xmlTexturesElement = xmlDoc.CreateElement("Textures");
            xmlBoxProperties.AppendChild(xmlTexturesElement);
        }
        public void Save(PalletProperties palletProperties, XmlElement parentElement, XmlDocument xmlDoc)
        {
            // create xmlPalletProperties element
            XmlElement xmlPalletProperties = xmlDoc.CreateElement("PalletProperties");
            parentElement.AppendChild(xmlPalletProperties);
            // Id
            XmlAttribute guidAttribute = xmlDoc.CreateAttribute("Id");
            guidAttribute.Value = palletProperties.Guid.ToString();
            xmlPalletProperties.Attributes.Append(guidAttribute);
            // name
            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Name");
            nameAttribute.Value = palletProperties.Name;
            xmlPalletProperties.Attributes.Append(nameAttribute);
            // description
            XmlAttribute descAttribute = xmlDoc.CreateAttribute("Description");
            descAttribute.Value = palletProperties.Description;
            xmlPalletProperties.Attributes.Append(descAttribute);
            // length
            XmlAttribute lengthAttribute = xmlDoc.CreateAttribute("Length");
            lengthAttribute.Value = string.Format("{0}", palletProperties.Length);
            xmlPalletProperties.Attributes.Append(lengthAttribute);
            // width
            XmlAttribute widthAttribute = xmlDoc.CreateAttribute("Width");
            widthAttribute.Value = string.Format("{0}", palletProperties.Width);
            xmlPalletProperties.Attributes.Append(widthAttribute);
            // height
            XmlAttribute heightAttribute = xmlDoc.CreateAttribute("Height");
            heightAttribute.Value = string.Format("{0}", palletProperties.Height);
            xmlPalletProperties.Attributes.Append(heightAttribute);
            // weight
            XmlAttribute weightAttribute = xmlDoc.CreateAttribute("Weight");
            weightAttribute.Value = string.Format("{0}", palletProperties.Weight);
            xmlPalletProperties.Attributes.Append(weightAttribute);
            // type
            XmlAttribute typeAttribute = xmlDoc.CreateAttribute("Type");
            typeAttribute.Value = string.Format("{0}", (int)palletProperties.Type);
            xmlPalletProperties.Attributes.Append(typeAttribute);
            // color
            XmlAttribute colorAttribute = xmlDoc.CreateAttribute("Color");
            colorAttribute.Value = string.Format("{0}", palletProperties.Color.ToArgb());
            xmlPalletProperties.Attributes.Append(colorAttribute);
        }
        public void Save(InterlayerProperties interlayerProperties, XmlElement parentElement, XmlDocument xmlDoc)
        {
            // create xmlPalletProperties element
            XmlElement xmlInterlayerProperties = xmlDoc.CreateElement("InterlayerProperties");
            parentElement.AppendChild(xmlInterlayerProperties);
            // Id
            XmlAttribute guidAttribute = xmlDoc.CreateAttribute("Id");
            guidAttribute.Value = interlayerProperties.Guid.ToString();
            xmlInterlayerProperties.Attributes.Append(guidAttribute);
            // name
            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Name");
            nameAttribute.Value = interlayerProperties.Name;
            xmlInterlayerProperties.Attributes.Append(nameAttribute);
            // description
            XmlAttribute descAttribute = xmlDoc.CreateAttribute("Description");
            descAttribute.Value = interlayerProperties.Description;
            xmlInterlayerProperties.Attributes.Append(descAttribute);
            // length
            XmlAttribute lengthAttribute = xmlDoc.CreateAttribute("Length");
            lengthAttribute.Value = string.Format("{0}", interlayerProperties.Length);
            xmlInterlayerProperties.Attributes.Append(lengthAttribute);
            // width
            XmlAttribute widthAttribute = xmlDoc.CreateAttribute("Width");
            widthAttribute.Value = string.Format("{0}", interlayerProperties.Width);
            xmlInterlayerProperties.Attributes.Append(widthAttribute);
            // height
            XmlAttribute heightAttribute = xmlDoc.CreateAttribute("Height");
            heightAttribute.Value = string.Format("{0}", interlayerProperties.Thickness);
            xmlInterlayerProperties.Attributes.Append(heightAttribute);
            // weight
            XmlAttribute weightAttribute = xmlDoc.CreateAttribute("Weight");
            weightAttribute.Value = string.Format("{0}", interlayerProperties.Weight);
            xmlInterlayerProperties.Attributes.Append(weightAttribute);
            // color
            XmlAttribute colorAttribute = xmlDoc.CreateAttribute("Color");
            colorAttribute.Value = string.Format("{0}", interlayerProperties.Color.ToArgb());
            xmlInterlayerProperties.Attributes.Append(colorAttribute);
        }
        public void Save(BundleProperties bundleProperties, XmlElement parentElement, XmlDocument xmlDoc)
        {
            // create xmlPalletProperties element
            XmlElement xmlInterlayerProperties = xmlDoc.CreateElement("BundleProperties");
            parentElement.AppendChild(xmlInterlayerProperties);
            // Id
            XmlAttribute guidAttribute = xmlDoc.CreateAttribute("Id");
            guidAttribute.Value = bundleProperties.Guid.ToString();
            xmlInterlayerProperties.Attributes.Append(guidAttribute);
            // name
            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Name");
            nameAttribute.Value = bundleProperties.Name;
            xmlInterlayerProperties.Attributes.Append(nameAttribute);
            // description
            XmlAttribute descAttribute = xmlDoc.CreateAttribute("Description");
            descAttribute.Value = bundleProperties.Description;
            xmlInterlayerProperties.Attributes.Append(descAttribute);
            // length
            XmlAttribute lengthAttribute = xmlDoc.CreateAttribute("Length");
            lengthAttribute.Value = string.Format("{0}", bundleProperties.Length);
            xmlInterlayerProperties.Attributes.Append(lengthAttribute);
            // width
            XmlAttribute widthAttribute = xmlDoc.CreateAttribute("Width");
            widthAttribute.Value = string.Format("{0}", bundleProperties.Width);
            xmlInterlayerProperties.Attributes.Append(widthAttribute);
            // height
            XmlAttribute heightAttribute = xmlDoc.CreateAttribute("UnitThickness");
            heightAttribute.Value = string.Format("{0}", bundleProperties.UnitThickness);
            xmlInterlayerProperties.Attributes.Append(heightAttribute);
            // weight
            XmlAttribute weightAttribute = xmlDoc.CreateAttribute("UnitWeight");
            weightAttribute.Value = string.Format("{0}", bundleProperties.UnitWeight);
            xmlInterlayerProperties.Attributes.Append(weightAttribute);
            // color
            XmlAttribute colorAttribute = xmlDoc.CreateAttribute("Color");
            colorAttribute.Value = string.Format("{0}", bundleProperties.Color.ToArgb());
            xmlInterlayerProperties.Attributes.Append(colorAttribute);
            // numberFlats
            XmlAttribute numberFlatsAttribute = xmlDoc.CreateAttribute("NumberFlats");
            numberFlatsAttribute.Value = string.Format("{0}", bundleProperties.NoFlats);
            xmlInterlayerProperties.Attributes.Append(numberFlatsAttribute);

        }
        public void Save(TruckProperties truckProperties, XmlElement parentElement, XmlDocument xmlDoc)
        { 
        }
        public void Save(Analysis analysis, XmlElement parentElement, XmlDocument xmlDoc)
        {
            // create analysis element
            XmlElement xmlAnalysisElt = xmlDoc.CreateElement("AnalysisPallet");
            parentElement.AppendChild(xmlAnalysisElt);
            // Name
            XmlAttribute analysisNameAttribute = xmlDoc.CreateAttribute("Name");
            analysisNameAttribute.Value = analysis.Name;
            xmlAnalysisElt.Attributes.Append(analysisNameAttribute);
            // Description
            XmlAttribute analysisDescriptionAttribute = xmlDoc.CreateAttribute("Description");
            analysisDescriptionAttribute.Value = analysis.Description;
            xmlAnalysisElt.Attributes.Append(analysisDescriptionAttribute);
            // BoxId
            XmlAttribute boxIdAttribute = xmlDoc.CreateAttribute("BoxId");
            boxIdAttribute.Value = string.Format("{0}", analysis.BoxProperties.Guid);
            xmlAnalysisElt.Attributes.Append(boxIdAttribute);
            // PalletId
            XmlAttribute palletIdAttribute = xmlDoc.CreateAttribute("PalletId");
            palletIdAttribute.Value = string.Format("{0}", analysis.PalletProperties.Guid);
            xmlAnalysisElt.Attributes.Append(palletIdAttribute);
            // InterlayerId
            if (null != analysis.InterlayerProperties)
            {
                XmlAttribute interlayerIdAttribute = xmlDoc.CreateAttribute("InterlayerId");
                interlayerIdAttribute.Value = string.Format("{0}", analysis.InterlayerProperties.Guid);
                xmlAnalysisElt.Attributes.Append(interlayerIdAttribute);
            }
            // ConstraintSet
            XmlElement constraintSetElement = xmlDoc.CreateElement("ConstraintSet");
            xmlAnalysisElt.AppendChild(constraintSetElement);

            // Solutions
            XmlElement solutionsElt = xmlDoc.CreateElement("Solutions");
            xmlAnalysisElt.AppendChild(solutionsElt);
            foreach (Solution sol in analysis.Solutions)
            {
                // Solution
                XmlElement solutionElt = xmlDoc.CreateElement("Solution");
                solutionsElt.AppendChild(solutionElt);
                // title
                XmlAttribute titleAttribute = xmlDoc.CreateAttribute("Title");
                titleAttribute.Value = sol.Title;
                solutionElt.Attributes.Append(titleAttribute);
                // layers
                XmlElement layersElt = xmlDoc.CreateElement("Layers");
                solutionElt.AppendChild(layersElt);
                foreach (ILayer layer in sol)
                {
                    BoxLayer boxLayer = layer as BoxLayer;
                    if (null != boxLayer)
                    {
                        // BoxLayer
                        XmlElement boxlayerElt = xmlDoc.CreateElement("BoxLayer");
                        layersElt.AppendChild(boxlayerElt);
                        // ZLow
                        XmlAttribute zlowAttribute = xmlDoc.CreateAttribute("ZLow");
                        zlowAttribute.Value = string.Format("{0}", boxLayer.ZLow);
                        boxlayerElt.Attributes.Append(zlowAttribute);
                        foreach (BoxPosition boxPosition in boxLayer)
                        {
                            // BoxPosition
                            XmlElement boxPositionElt = xmlDoc.CreateElement("BoxPosition");
                            boxlayerElt.AppendChild(boxPositionElt);
                            // Position
                            XmlAttribute positionAttribute = xmlDoc.CreateAttribute("Position");
                            positionAttribute.Value = string.Format("{0} {1} {2}"
                                , boxPosition.Position.X
                                , boxPosition.Position.Y
                                , boxPosition.Position.Z);
                            boxPositionElt.Attributes.Append(positionAttribute);
                            // AxisLength
                            XmlAttribute axisLengthAttribute = xmlDoc.CreateAttribute("AxisLength");
                            axisLengthAttribute.Value = AxisToEnum(boxPosition.DirectionLength);
                            boxPositionElt.Attributes.Append(axisLengthAttribute);
                            // AxisWidth
                            XmlAttribute axisWidthAttribute = xmlDoc.CreateAttribute("AxisWidth");
                            axisWidthAttribute.Value = AxisToEnum(boxPosition.DirectionWidth);
                            boxPositionElt.Attributes.Append(axisWidthAttribute);
                        }
                    }
                    InterlayerPos interlayerPos = layer as InterlayerPos;
                    if (null != interlayerPos)
                    {
                        // Interlayer
                        XmlElement interlayerElt = xmlDoc.CreateElement("Interlayer");
                        layersElt.AppendChild(interlayerElt);
                        // ZLow
                        XmlAttribute zlowAttribute = xmlDoc.CreateAttribute("ZLow");
                        zlowAttribute.Value = string.Format("{0}", interlayerPos.ZLow);
                        interlayerElt.Attributes.Append(zlowAttribute);
                    }
                }
            }
        }

        private string AxisToEnum(HalfAxis axis)
        {
            switch (axis)
            {
                case HalfAxis.AXIS_X_N: return "XN";
                case HalfAxis.AXIS_X_P: return "XP";
                case HalfAxis.AXIS_Y_N: return "YN";
                case HalfAxis.AXIS_Y_P: return "YP";
                case HalfAxis.AXIS_Z_N: return "ZN";
                default: return "ZP";
            }
        }
        #endregion

        #region Listener related methods
        public void AddListener(IDocumentListener listener)
        {
            _listeners.Add(listener);
            listener.OnNewDocument(this);
        }
        private void NotifyOnNewTypeCreated(ItemProperties itemProperties)
        {
            foreach (IDocumentListener listener in _listeners)
                listener.OnNewTypeCreated(this, itemProperties);
        }
        private void NotifyOnNewAnalysisCreated(Analysis analysis)
        {
            foreach (IDocumentListener listener in _listeners)
                listener.OnNewAnalysisCreated(this, analysis);
        }
        #endregion
    }
    #endregion
}
