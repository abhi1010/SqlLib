using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DigestLib.Data
{
    public class LibFieldCollection : IEnumerator<LibField>, IDisposable, IEnumerator, IEnumerable<LibField>, IEnumerable
    {
        private List<LibField> list;
        private int position = -1;
        private ArrayList LibNames;
        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }
        public ArrayList Keys
        {
            get
            {
                return this.LibNames;
            }
        }
        public LibField this[int index]
        {
            get
            {
                return this.list[index];
            }
        }
        public LibField this[string index]
        {
            get
            {
                int num = this.LibNames.IndexOf(index);
                LibField result;
                if (num != -1)
                {
                    result = this.list[num];
                }
                else
                {
                    result = null;
                }
                return result;
            }
        }
        public LibField Current
        {
            get
            {
                return this.list[this.position];
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return this.list[this.position];
            }
        }
        LibField IEnumerator<LibField>.Current
        {
            get
            {
                return this.list[this.position];
            }
        }
        public LibFieldCollection()
        {
            this.list = new List<LibField>();
            this.LibNames = new ArrayList();
        }
        public LibFieldCollection(ControlCollection coll)
            : this()
        {
            this.FindChildren(coll);
        }
        private void FindChildren(ControlCollection controlColl)
        {
            foreach (Control control in controlColl)
            {
                if (control.HasControls())
                {
                    this.FindChildren(control.Controls);
                }
                this.AddControl(control);
            }
        }
        private void AddControl(Control control)
        {
            string text = control.GetType().ToString();
            switch (text)
            {
                case "System.Web.UI.WebControls.RadioButton":
                    {
                        RadioButton radioButton = (RadioButton)control;
                        this.Add(new LibField(radioButton.ID, radioButton.Enabled.ToString()));
                        break;
                    }
                case "System.Web.UI.WebControls.DropDownList":
                    {
                        DropDownList dropDownList = (DropDownList)control;
                        this.Add(new LibField(dropDownList.ID, dropDownList.SelectedValue.ToString()));
                        break;
                    }
                case "System.Web.UI.WebControls.Calendar":
                    {
                        Calendar calendar = (Calendar)control;
                        this.Add(new LibField(calendar.ID, calendar.SelectedDate.ToString(), typeof(DateTime)));
                        break;
                    }
                case "System.Web.UI.WebControls.CheckBox":
                    {
                        CheckBox checkBox = (CheckBox)control;
                        this.Add(new LibField(checkBox.ID, checkBox.Checked.ToString()));
                        break;
                    }
                case "System.Web.UI.WebControls.TextBox":
                    {
                        TextBox textBox = (TextBox)control;
                        this.Add(new LibField(textBox.ID, textBox.Text.ToString()));
                        break;
                    }
                case "System.Web.UI.WebControls.HiddenField":
                    {
                        HiddenField hiddenField = (HiddenField)control;
                        this.Add(new LibField(hiddenField.ID, hiddenField.Value.ToString()));
                        break;
                    }
                case "System.Web.UI.WebControls.CheckBoxList":
                    {
                        CheckBoxList checkBoxList = (CheckBoxList)control;
                        string text2 = "";
                        foreach (ListItem listItem in checkBoxList.Items)
                        {
                            if (listItem.Selected)
                            {
                                text2 = text2 + listItem.Value + ", ";
                            }
                        }
                        if (text2.EndsWith(", "))
                        {
                            text2 = text2.Substring(0, text2.Length - 2);
                        }
                        this.Add(new LibField(checkBoxList.ID, text2));
                        break;
                    }
                case "System.Web.UI.WebControls.RadioButtonList":
                    {
                        RadioButtonList radioButtonList = (RadioButtonList)control;
                        this.Add(new LibField(radioButtonList.ID, radioButtonList.SelectedValue.ToString()));
                        break;
                    }
                case "System.Web.UI.WebControls.ListBox":
                    {
                        ListBox listBox = (ListBox)control;
                        this.Add(new LibField(listBox.ID, listBox.SelectedValue.ToString()));
                        break;
                    }
                case "FredCK.FCKeditorV2.FCKeditor":
                    {
                        //FCKeditor fCKeditor = (FCKeditor)control;
                        //this.Add(new LibField(fCKeditor.ID, fCKeditor.get_Value()));
                        break;
                    }
                case "OboutInc.Editor.Editor":
                    {
                        //Editor editor = (Editor)control;
                        //this.Add(new LibField(editor.ID, editor.get_Content()));
                        break;
                    }
                case "DigestLib.Web.UI.ExtendedControls.LibTextArea":
                    {
                        //LibTextArea LibTextArea = (LibTextArea)control;
                        //this.Add(new LibField(LibTextArea.ID, LibTextArea.Text));
                        break;
                    }
            }
        }
        public LibFieldCollection(NameValueCollection nvColl)
            : this()
        {
            foreach (string name in nvColl)
            {
                this.Add(new LibField(name, nvColl[name]));
            }
        }
        public LibFieldCollection(LibField LibData)
            : this()
        {
            this.Add(LibData);
        }
        public LibFieldCollection(params LibField[] LibDatas)
            : this()
        {
            for (int i = 0; i < LibDatas.Length; i++)
            {
                LibField LibData = LibDatas[i];
                this.Add(LibData);
            }
        }
        private string GenHtmlTable()
        {
            return "<table border=1>\r\n                        <tr>\r\n                            <td>r1c1</td>\r\n                            <td>r1c2</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>r2c1</td>\r\n                            <td>r2c2</td>\r\n                        </tr>\r\n                    </table>";
        }
        public void Clear()
        {
            this.list.Clear();
            this.LibNames.Clear();
        }
        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
            this.LibNames.RemoveAt(index);
        }
        public void RemoveRange(int index, int count)
        {
            this.list.RemoveRange(index, count);
            this.LibNames.RemoveRange(index, count);
        }
        public bool Contains(LibField LibData)
        {
            return this.list.Contains(LibData);
        }
        public bool Contains(string fieldName)
        {
            return this.LibNames.Contains(fieldName);
        }
        public NameValueCollection GetAsNameValueCollection()
        {
            NameValueCollection nameValueCollection = new NameValueCollection(this.list.Count);
            for (int i = 0; i < this.list.Count; i++)
            {
                nameValueCollection.Add(this.list[i].Name, this.list[i].Value);
            }
            return nameValueCollection;
        }
        public int Add(LibField LibData)
        {
            LibData.NameChanged += new LibFieldNameEventHandler(this.LibData_NameChanged);
            this.list.Add(LibData);
            return this.LibNames.Add(LibData.Name);
        }
        private void LibData_NameChanged(string prevName, string newName)
        {
            int num = this.LibNames.IndexOf(prevName);
            if (num != -1)
            {
                this.LibNames[num] = newName;
            }
        }
        public int Add(string name, string value)
        {
            LibField LibData = new LibField(name, value);
            return this.Add(LibData);
        }
        public bool Remove(LibField LibData)
        {
            return this.list.Remove(LibData);
        }
        public void Dispose()
        {
        }
        public bool MoveNext()
        {
            this.position++;
            return this.position < this.list.Count;
        }
        public void Reset()
        {
            this.position = -1;
        }
        public IEnumerator<LibField> GetEnumerator()
        {
            this.Reset();
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        void IDisposable.Dispose()
        {
            this.Dispose();
        }
        bool IEnumerator.MoveNext()
        {
            this.position++;
            return this.position < this.list.Count;
        }
        void IEnumerator.Reset()
        {
            this.position = -1;
        }
        IEnumerator<LibField> IEnumerable<LibField>.GetEnumerator()
        {
            return this;
        }
    }
}
