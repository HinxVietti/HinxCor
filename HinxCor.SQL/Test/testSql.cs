using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public class testSql
{
    public static void TestConnection()
    {
        OpenFileDialog openFile = new OpenFileDialog();
        openFile.Filter = "DB文件|*.db";
        openFile.ShowDialog();
        var dbFile = openFile.FileName;
        var DBRoot = new SqlDB(dbFile);
        var helper = DBRoot.CreateHelper();

        string dbName = "TestImageDB";

        //OpenFileDialog open = new OpenFileDialog();
        //open.Filter = "";
        //open.ShowDialog();
        //var img = Image.FromFile(open.FileName);
        //var ms = new MemoryStream();
        //img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //var buffer = new byte[ms.Length];
        //ms.Position = 0;
        //ms.Read(buffer, 0, buffer.Length);
        //savePicture(DBRoot.SQLiteConnection, buffer, dbName);

        //var str = Convert.ToBase64String(buffer);

        //helper.ExecuteQuery(string.Format("insert into {0} values('6','Ming','{1}')", dbName, str));

        var reader = helper.ExecuteQuery(string.Format("select * from {0} where ID != 0", dbName));
        if (reader.Read())
        {
            //int x = reader.GetOrdinal("data");
            //int size = 50 * 1024 * 1024;
            //var buffer = new byte[size];
            //var blod = reader.GetBytes(x, 0, buffer, 0, size);
            //var bda = new byte[blod];

            //Array.Copy(buffer, bda, blod);

            ////buffer.CopyTo(bda, 0);
            
            //File.WriteAllBytes("dlImag33333333e.png", bda);

            //var blod = reader.GetString(x);
            //var blod = reader.GetBlob(x, false);
            //var boldStr = blod.ToString();
            //var pngBD = Convert.FromBase64String(blod);
            //File.WriteAllBytes("dlImage.png", pngBD);

        //int x = reader.get

        }

        //File.WriteAllText("img", str);
        //var bs = File.ReadAllText("img");
        //var pngBD = Convert.FromBase64String(bs);
        //File.WriteAllBytes("new.png", pngBD);

    }

    private static void savePicture(SQLiteConnection cnn, object data, string dbName)
    {
        cnn.Open();
        using (SQLiteCommand cmd = cnn.CreateCommand())
        {
            //cmd.CommandText = "Create Table test(data Image)"; 
            //cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format("insert into {0} values('12','小明','@data')", dbName);

            SQLiteParameter para = new SQLiteParameter("@data", DbType.Binary);
            //string file = @"F:/Image/飞机.png";
            //FileStream fs = new FileStream(file, FileMode.Open);

            //StreamUtil su = new StreamUtil();
            //byte[] buffer = su.StreamToBytes(fs);
            //byte[] buffer = StreamUtil.ReadFully(fs);

            //fs.Close();

            para.Value = data;
            cmd.Parameters.Add(para);
            cmd.ExecuteNonQuery();
        }
    }

    private static void savePicture()
    {
        using (SQLiteConnection cnn = new SQLiteConnection(""))
        {
            cnn.Open();
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                //cmd.CommandText = "Create Table test(data Image)"; 
                //cmd.ExecuteNonQuery();

                cmd.CommandText = "insert into person values('12',@data,'14','13')";
                SQLiteParameter para = new SQLiteParameter("@data", DbType.Binary);
                string file = @"F:/Image/飞机.png";
                FileStream fs = new FileStream(file, FileMode.Open);

                //StreamUtil su = new StreamUtil();
                //byte[] buffer = su.StreamToBytes(fs);
                byte[] buffer = StreamUtil.ReadFully(fs);

                fs.Close();

                para.Value = buffer;
                cmd.Parameters.Add(para);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static void GetS()
    {
        FileStream m_filestream = null;

        try
        {

            m_filestream = new FileStream(@"d:/pcinfo/17.jpg", FileMode.Open, FileAccess.Read); //读取图片 

            SQLiteCommand m_commd2 = new SQLiteCommand();
            m_commd2.CommandText = "UPDATE test1 set timage=@idimage WHERE tparendid=78";


            Byte[] m_byte = new Byte[m_filestream.Length]; //存放图片 

            m_filestream.Read(m_byte, 0, m_byte.Length);

            m_filestream.Close();

            SQLiteParameter param_m = new SQLiteParameter("@idimage", DbType.Binary, m_byte.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, m_byte);
            m_commd2.Parameters.Add(param_m); m_commd2.Parameters.Add(param_m); //很多参数阿，注意DBType.Binary 

            //m_commd2.Connection = m_conn;
            m_commd2.ExecuteNonQuery();


        }
        catch (SQLiteException ex)
        {

            MessageBox.Show("未能存入图片");

        }
    }


    public static class StreamUtil
    {
        const int BufferSize = 8192;

        public static void CopyTo(Stream input, Stream output)
        {
            byte[] buffer = new byte[BufferSize];

            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream tempStream = new MemoryStream())
            {
                CopyTo(input, tempStream);
                return tempStream.ToArray();
            }
        }

    }

    #region test
    //public int InsertImage(SQLiteHelper helper)
    //{
    //    DataRow dataRow = null;
    //    var isSucces = false;
    //    var image = LoadImage();//if no file was selected and no image was created return 0if(image ==null)return0;if(image !=null){// Determin the ConnectionStringstring connectionString = dBFunctions.ConnectionStringSQLite;// Determin the DataAdapter = CommandText + Connectionstring commandText ="SELECT * FROM ImageStore WHERE 1=0";// Make a new object
    //    //helper = new dBHelper(connectionString);
    //    {// Load Dataif(helper.Load(commandText,"image_id")==true){// Add a row and determin the row
    //        helper.DataSet.Tables[0].Rows.Add(
    //                   helper.DataSet.Tables[0].NewRow());
    //        dataRow = helper.DataSet.Tables[0].Rows[0];// Enter the given values
    //        dataRow["imageFileName"] = image.FileName;
    //        dataRow["imageBlob"] = image.ImageData;
    //        dataRow["imageFileSizeBytes"] = image.FileSize;
    //        try
    //        {// Save -> determin succes
    //            if (helper.Save() == true)
    //            {
    //                isSucces = true;
    //            }
    //            else
    //            {
    //                isSucces = false; MessageBox.Show("Error during Insertion");
    //            }
    //        }
    //        catch (Exception ex)
    //        {// Show the Exception --> Dubbel Id/Name ?MessageBox.Show(ex.Message);}}//END IF}}//return the new image_idreturnConvert.ToInt32(dataRow[0].ToString());}
    //        }
    //        return 0;
    //    }
    //}

    //private Image LoadImage()
    //{
    //    Image image = null;
    //    //Create an instance of the Image Class/Object//so that we can store the information about the picture an send it back for
    //    //processing into the database.Image image =null;//Ask user to select 
    //    var dlg = new OpenFileDialog();
    //    dlg.InitialDirectory = @"C:\\";
    //    dlg.Title = "Select Image File";
    //    //dlg.Filter = "Tag Image File Format (*.tiff)|*.tiff";//dlg.Filter += "|Graphics Interchange Format (*.gif)|*.gif";
    //    //dlg.Filter += "|Portable Network Graphic Format (*.png)|*.png";//dlg.Filter += "|Joint Photographic Experts Group Format (*.jpg)|*.jpg";
    //    //dlg.Filter += "|Joint Photographic Experts Group Format (*.jpeg)|*.jpeg";
    //    //dlg.Filter += "|Nikon Electronic Format (*.nef)|*.nef";//dlg.Filter += "|All files (*.*)|*.*";
    //    dlg.Filter = "Image Files  (*.jpg ; *.jpeg ; *.png ; *.gif ; *.tiff ; *.nef) | *.jpg; *.jpeg; *.png; *.gif; *.tiff; *.nef";
    //    dlg.ShowDialog();
    //    var FileLocation = dlg.FileName;
    //    if (FileLocation == null || FileLocation == string.Empty)
    //        return null;
    //    if (FileLocation != string.Empty)
    //    {
    //        Cursor.Current = Cursors.WaitCursor;
    //        //Get file information and calculate the filesize
    //        FileInfo info = new FileInfo(FileLocation);
    //        long fileSize = info.Length;
    //        //reasign the filesize to calculated filesize
    //        var maxImageSize = (Int32)fileSize; if (File.Exists(FileLocation))
    //        {
    //            //Retreave image from file and binary it to Object image
    //            using (FileStream stream = File.Open(FileLocation, FileMode.Open))
    //            {
    //                BinaryReader br = new BinaryReader(stream);
    //                byte[] data = br.ReadBytes(maxImageSize);
    //                //image = new Image(dlg.SafeFileName, data, fileSize);
    //                image = Image.FromFile(dlg.FileName);
    //            }
    //        }
    //        Cursor.Current = Cursors.Default;
    //    }
    //    return image;
    //}

    //public void SaveAsImage(Int32 imageID)
    //{
    //    //set variables
    //    DataRow dataRow = null;
    //    Image image = null;
    //    var isSucces = false;// Displays a SaveFileDialog so the user can save the 
    //    var dlg = new SaveFileDialog();
    //    dlg.InitialDirectory = @"C:\\";
    //    dlg.Title = "Save Image File";//1
    //    dlg.Filter = "Tag Image File Format (*.tiff)|*.tiff";//2
    //    dlg.Filter += "|Graphics Interchange Format (*.gif)|*.gif";//3
    //    dlg.Filter += "|Portable Network Graphic Format (*.png)|*.png";//4
    //    dlg.Filter += "|Joint Photographic Experts Group Format (*.jpg)|*.jpg";//5
    //    dlg.Filter += "|Joint Photographic Experts Group Format (*.jpeg)|*.jpeg";//6
    //    dlg.Filter += "|Bitmap Image File Format (*.bmp)|*.bmp";//7
    //    dlg.Filter += "|Nikon Electronic Format (*.nef)|*.nef";
    //    dlg.ShowDialog();
    //    // If the file name is not an empty string open it for saving.
    //    if (dlg.FileName != "")
    //    {
    //        Cursor.Current = Cursors.WaitCursor;
    //        //making shore only one of the 7 is being used.
    //        //if not added the default extention to the filename
    //        string defaultExt = ".png";
    //        int pos = -1;
    //        string[] ext = new string[7] { ".tiff", ".gif", ".png", ".jpg", ".jpeg", ".bmp", ".nef" };
    //        string extFound = string.Empty;
    //        string filename = dlg.FileName.Trim();
    //        for (int i = 0; i < ext.Length; i++)
    //        {
    //            var pos = filename.IndexOf(ext[i], pos + 1); if (pos > -1)
    //            {
    //                extFound = ext[i]; break;
    //            }
    //        }
    //        if (extFound == string.Empty) filename = filename + defaultExt;
    //        // Determin the ConnectionString
    //        string connectionString = dBFunctions.ConnectionStringSQLite;// Determin the   DataAdapter = CommandText + Connection;
    //        string commandText = "SELECT * FROM ImageStore WHERE image_id=" + imageID;
    //        // Make a new object
    //        helper = new dBHelper(connectionString);// Load the data
    //        if (helper.Load(commandText, "") == true)
    //        {
    //            // Show the data in the datagridview
    //            dataRow = helper.DataSet.Tables[0].Rows[0];
    //            image = newImage((string)dataRow["imageFileName"], (byte[])dataRow["imageBlob"], (long)dataRow["imageFileSizeBytes"]);
    //            // Saves the Image via a FileStream created by the OpenFile method.
    //            using (FileStream stream = newFileStream(filename, FileMode.Create))
    //            {
    //                BinaryWriter bw = new BinaryWriter(stream);
    //                bw.Write(image.ImageData);
    //                isSucces = true;
    //            }
    //        }
    //        Cursor.Current = Cursors.Default;
    //    }
    //    if (isSucces) { MessageBox.Show("Save succesfull"); } else { MessageBox.Show("Save failed"); }
    //}

    #endregion
}

