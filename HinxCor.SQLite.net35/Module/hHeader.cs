using System;
using System.Collections.Generic;

public class hHeader
{
    public FieldType FieldType { get; set; }
    public string FieldName;

    public bool UNIQUE { get; set; } = false;
    public bool PRIMARY_KEY { get; set; } = false;
    public bool AUTOINCREMENT { get; set; } = false;
    public bool NOTNULL { get; set; } = false;
    public Collate COLLATE { get; set; } = Collate.none;
    public string DefaultValue { get; set; } = "";

    public hHeader(FieldType fieldType = FieldType.INTEGER, string fieldname = "", bool unique = false,
        bool primarykey = false, bool autoincrement = false,
        bool notnull = false, Collate collate = Collate.none, string defaultValue = "")
    {
        this.FieldType = fieldType;
        this.FieldName = fieldname;
        this.UNIQUE = unique;
        this.PRIMARY_KEY = primarykey;
        this.AUTOINCREMENT = autoincrement;
        this.NOTNULL = notnull;
        this.COLLATE = collate;
        this.DefaultValue = defaultValue;
    }

    public override string ToString()
    {
        string.Format("{0} {1} {2} {3} {4} {5} {6} {7} ", FieldName, FieldType.GetString(),
            (PRIMARY_KEY ? "PRIMARY" : ""),
            (AUTOINCREMENT ? "AUTOINCREMENT" : ""),
            UNIQUE ? "" : "",
            NOTNULL ? "" : "",
            GetCollateString(COLLATE),
            string.IsNullOrEmpty(DefaultValue) ? "" : GetDefaultValueString(DefaultValue)
            );

        return base.ToString();
    }

    private string GetDefaultValueString(string defaultValue)
    {
        return string.Format("DEFAULT ({0})", defaultValue);
    }

    private string GetCollateString(Collate collate)
    {
        switch (collate)
        {
            case Collate.none:
                return string.Empty;
            case Collate.RTRIM:
                return "COLLATE RTRIM";
            case Collate.NOCASE:
                return "COLLATE NOCASE";
            case Collate.BINARY:
                return "COLLATE BINARY";
            default:
                return string.Empty;
        }
    }

    public enum Collate
    {
        none,
        RTRIM,
        NOCASE,
        BINARY
    }
}

