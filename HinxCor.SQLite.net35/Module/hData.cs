using System;
using System.Collections.Generic;

public class hData
{

}


public static partial class Helper
{
    
    public static string GetString(this FieldType type)
    {
        switch (type)
        {

            case FieldType.UNSIGNED_BIG_INT:
                return "UNSIGNED BIG INT";
            case FieldType.CHARACTER_20:
                return "CHARACTER(20)";
            case FieldType.VARCHAR_255:
                return "VARCHAR(255)";
            case FieldType.VARYING_CHARACTER_255:
                return "CHARACTER(255)";
            case FieldType.NCHAR_55:
                return "NCHAR(55)";
            case FieldType.NATIVE_CHARACTER_70:
                return "NATIVE CHARACTER(70)";
            case FieldType.NVARCHAR_100:
                return "NVARCHAR(100)";
            case FieldType.no_datatype_special:
                return "NONE";
            case FieldType.BOUDLE_PRECISION:
                return "DOUBLE PRECISION";
            case FieldType.DECIMAL_10_5:
                return "DECIMAL(10,5)";
            default:
                return type.ToString();
        }
    }
}

public enum FieldType
{
    /// <summary>
    /// INTEGER
    /// </summary>
    INT,
    /// <summary>
    /// INTEGER
    /// </summary>
    INTEGER,
    /// <summary>
    /// INTEGER
    /// </summary>
    TINYINT,
    /// <summary>
    /// INTEGER
    /// </summary>
    SMALLINT,
    /// <summary>
    /// INTEGER
    /// </summary>
    MEDIUMINT,
    /// <summary>
    /// INTEGER
    /// </summary>
    BIGINT,
    /// <summary>
    /// INTEGER
    /// </summary>
    UNSIGNED_BIG_INT,
    /// <summary>
    /// INTEGER
    /// </summary>
    INT2,
    /// <summary>
    /// INTEGER
    /// </summary>
    INT8,

    /// <summary>
    /// TEXT
    /// </summary>
    CHARACTER_20,
    /// <summary>
    /// TEXT
    /// </summary>
    VARCHAR_255,
    /// <summary>
    /// TEXT
    /// </summary>
    VARYING_CHARACTER_255,
    /// <summary>
    /// TEXT
    /// </summary>
    NCHAR_55,
    /// <summary>
    /// TEXT
    /// </summary>
    NATIVE_CHARACTER_70,
    /// <summary>
    /// TEXT
    /// </summary>
    NVARCHAR_100,
    /// <summary>
    /// TEXT
    /// </summary>
    STRING,
    TEXT,
    /// <summary>
    /// TEXT
    /// </summary>
    CLOB,

    /// <summary>
    /// NONE
    /// </summary>
    BLOB,
    /// <summary>
    /// NONE
    /// </summary>
    no_datatype_special,

    /// <summary>
    /// REAL
    /// </summary>
    REAL,
    /// <summary>
    /// REAL
    /// </summary>
    DOUBLE,
    /// <summary>
    /// REAL
    /// </summary>
    BOUDLE_PRECISION,
    /// <summary>
    /// REAL
    /// </summary>
    FLOAT,

    /// <summary>
    /// NUMERIC
    /// </summary>
    NUMERIC,
    /// <summary>
    /// NUMERIC
    /// </summary>
    DECIMAL_10_5,
    /// <summary>
    /// NUMERIC
    /// </summary>
    BOOLEAN,
    /// <summary>
    /// NUMERIC
    /// </summary>
    DATE,
    /// <summary>
    /// NUMERIC
    /// </summary>
    DATETIME
}
