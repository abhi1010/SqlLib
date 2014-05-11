using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace DigestLib.Data
{
    public delegate void LibFieldNameEventHandler(string prevName, string newName);


    public class LibField
    {
        private string _name;
        private Type _type;
        private string _value = "";
        private int _maxLength = -1;
        private int _minLength = -1;
        private Regex _regex;
        private string _errorMsg = null;
        public event LibFieldNameEventHandler NameChanged;

        public bool IsEmail
        {
            get
            {
                string pattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                return Regex.IsMatch(this._value, pattern);
            }
        }
        public bool IsNumber
        {
            get
            {
                Regex regex = new Regex("\\d");
                return regex.IsMatch(this._value);
            }
        }
        public bool IsDate
        {
            get
            {
                DateTime dateTime;
                return DateTime.TryParse(this._value, out dateTime);
            }
        }
        private bool IsDateKeyword
        {
            get
            {
                return this.Type == typeof(DateTime) && (this.Value.ToUpper().Contains("NOW") || this.Value.ToUpper().Contains("DATE()"));
            }
        }
        public bool IsValid
        {
            get
            {
                if (this.Type != null && this.Type == typeof(byte) && (this.Value == bool.TrueString || this.Value == bool.FalseString))
                {
                    if (this.Value == bool.TrueString)
                    {
                        this.Value = "1";
                    }
                    else
                    {
                        this.Value = "0";
                    }
                }
                bool result;
                if (this.MaxLength != -1 && this.Value.Length > this.MaxLength && !this.IsDateKeyword)
                {
                    this.ErrorMsg = string.Concat(new object[]
					{
						"Max Length does not match with ",
						this.MaxLength,
						" ... with TYPE=",
						this.Type.ToString()
					});
                    Console.WriteLine("Max Length does not match ");
                    result = false;
                }
                else
                {
                    if (this.MinLength != -1 && this.Value.Length < this.MinLength && !this.IsDateKeyword)
                    {
                        this.ErrorMsg = string.Concat(new object[]
						{
							"Min Length does not match with ",
							this.MinLength,
							" ... with TYPE=",
							this.Type.ToString()
						});
                        Console.WriteLine("Min Length does not match");
                        result = false;
                    }
                    else
                    {
                        if (this.Regex != null && this.Regex.Matches(this.Value).Count == 0)
                        {
                            this.ErrorMsg = string.Concat(new object[]
							{
								"Regex does not match with ",
								this.Regex,
								" ... with TYPE=",
								this.Type.ToString()
							});
                            Console.WriteLine("Regex does not match");
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
                return result;
            }
        }
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != null)
                {
                    this.OnNameChanged(this._name, value);
                }
                this._name = value;
            }
        }
        public Type Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        public string ErrorMsg
        {
            get
            {
                return this._errorMsg;
            }
            set
            {
                if (this._errorMsg == null)
                {
                    this._errorMsg = "";
                }
                this._errorMsg += value;
            }
        }
        public string Value
        {
            get
            {
                string result;
                if (this._type != null && this._type == typeof(DateTime) && this._value.ToUpper() != "NOW()" && this._value.ToUpper() != "GETDATE()" && this._value.Length > 0)
                {
                    result = DateTime.Parse(this._value).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    result = this._value;
                }
                return result;
            }
            set
            {
                this._value = value;
            }
        }
        public int MaxLength
        {
            get
            {
                return this._maxLength;
            }
            set
            {
                this._maxLength = value;
            }
        }
        public int MinLength
        {
            get
            {
                return this._minLength;
            }
            set
            {
                this._minLength = value;
            }
        }
        public Regex Regex
        {
            get
            {
                return this._regex;
            }
            set
            {
                this._regex = value;
            }
        }
        public string RegexPattern
        {
            get
            {
                return this._regex.ToString();
            }
            set
            {
                this._regex = new Regex(value);
            }
        }
        public void OnNameChanged(string prevName, string newName)
        {
            if (this.NameChanged != null)
            {
                this.NameChanged(prevName, newName);
            }
        }
        public LibField()
        {
        }
        public LibField(string name, string value)
        {
            this._name = name;
            this._value = value;
        }
        public LibField(string name, string value, Type type)
        {
            this._name = name;
            this._value = value;
            this._type = type;
        }
        public LibField(string name, string value, Type type, int minLength, int MaxLength)
        {
            this._name = name;
            this._value = value;
            this._type = type;
            this._minLength = minLength;
            this._maxLength = MaxLength;
        }
        public LibField(string name, string value, Type type, int minLength, int MaxLength, Regex regex)
        {
            this._name = name;
            this._value = value;
            this._type = type;
            this._minLength = minLength;
            this._maxLength = MaxLength;
            this._regex = regex;
        }
    }
}
