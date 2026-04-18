using MostWanted.Model;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;


namespace MostWanted.Services
{




    public class WantedPersonService
    {

        // fields
        SQLiteConnection conn;
        public string StatusMessage = string.Empty;
        string _dbPath;
        int result = 0;

        // string dbPath = string.Empty;
        // constructor
        public WantedPersonService(string dbPath)
        {
            _dbPath = dbPath;
          
            StatusMessage = string.Empty;
            Init();
        }

        private void Init()
        {



            //conn.DeleteAll<WantedPerson>();
            //conn.Execute("DELETE FROM sqlite_sequence WHERE name='WantedPerson'");

            if (conn != null)
            {
                int count = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM WantedPerson");
                Debug.WriteLine($"Table Records{count} Database Path:{_dbPath}");




                var columns = conn.GetTableInfo("WantedPerson");
                if (columns.Count > 0)
                {
                    Debug.WriteLine("[DB] WantedPerson table exists with columns:");
                    foreach (var col in columns)
                    {
                        Debug.WriteLine($" - {col.Name} ");
                    }
                }
                return;
            }




            else
            {
                Debug.WriteLine("[DB] WantedPerson table does NOT exist.");

                conn = new SQLiteConnection(_dbPath);

                conn.CreateTable<WantedPerson>();



            }
        }


        //private void Init()
        //{


        //    conn = new SQLiteConnection(_dbPath);
        //    //conn.DeleteAll<WantedPerson>();
        //    //conn.Execute("DELETE FROM sqlite_sequence WHERE name='WantedPerson'");

        //    if (conn != null)
        //    {

        //        Debug.WriteLine($"DB Connection: {conn}");
        //        return;
        //    }


        //    else
        //    {  conn = new SQLiteConnection(_dbPath);

        //        conn.CreateTable<WantedPerson>();

        //        Debug.WriteLine(" Table Created ");

        //        //if (!conn.Table<WantedPerson>().Any())
        //        //{
        //        //    conn.Insert(new WantedPerson { Id = 1, Name = "John Doe", Description = "Burglary suspect", Type = "Felony" });
        //        //    conn.Insert(new WantedPerson { Id = 2, Name = "Jane Smith", Description = "Fraud investigation", Type = "Felony" });
        //        //}}


        //        //  conn.CreateTable<WantedPerson>();

        //        //if (!conn.Table<WantedPerson>().Any())
        //        //{
        //        //    conn.Insert(new WantedPerson { Id = 1, Name = "John Doe", Description = "Burglary suspect", Type = "Felony" });
        //        //    conn.Insert(new WantedPerson { Id = 2, Name = "Jane Smith", Description = "Fraud investigation", Type = "Felony" });
        //        //}
        //    }
        //}

        public List<WantedPerson> GetWantedPersons()
        {



            try
            {
                Init();
                Debug.WriteLine($"Wanted Loaded {conn}");
                return conn.Table<WantedPerson>().ToList();

            }
            catch (Exception)
            {
                //throw;
                StatusMessage = "Failed to retrieve data";
            }
            

            return conn.Table<WantedPerson>().ToList();

            //return new List<WantedPerson>()
            //{
            //    new WantedPerson { Id = 1, Name = "John Doe", Description = "Suspected of burglary", Type = "Felony" },
            //    new WantedPerson { Id = 3, Name = "Carlos Ruiz", Description = "Armed robbery suspect", Type = "Felony" },
            //    new WantedPerson { Id = 4, Name = "Emily Johnson", Description = "Cybercrime investigation", Type = "Felony"},
            //    new WantedPerson { Id = 5, Name = "Michael Brown", Description = "Drug trafficking charges", Type = "Felony" },
            //    new WantedPerson { Id = 6, Name = "Sophia Lee", Description = "Identity theft suspect", Type = "Felony"},
            //    new WantedPerson { Id = 7, Name = "David Kim", Description = "Assault investigation", Type = "Felony"},
            //};
        }





        public WantedPerson GetWantedPersonInfo(int id)
        {

            Debug.WriteLine($"Hello: {id}");
            try
            {
                Init();
                return conn.Table<WantedPerson>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data";
              
            }
            return null;
        }


     

        //public List<WantedPerson> GetWantedPersonList()
        //{



        //    try
        //    {
        //        Init();
        //        Debug.WriteLine("Wanted Loaded");
        //        return conn.Table<WantedPerson>().ToList();

        //    }
        //    catch (Exception)
        //    {
        //        //throw;
        //        StatusMessage = "Failed to retrieve data";
        //    }
        //    // return new List<WantedPerson>();
        //    return new List<WantedPerson>()
        //    {
        //        new WantedPerson { Id = 1, Name = "John Doe", Description = "Suspected of burglary", Type = "Felony" },
        //        new WantedPerson { Id = 3, Name = "Carlos Ruiz", Description = "Armed robbery suspect", Type = "Felony" },
        //        new WantedPerson { Id = 4, Name = "Emily Johnson", Description = "Cybercrime investigation", Type = "Felony"},
        //        new WantedPerson { Id = 5, Name = "Michael Brown", Description = "Drug trafficking charges", Type = "Felony" },
        //        new WantedPerson { Id = 6, Name = "Sophia Lee", Description = "Identity theft suspect", Type = "Felony"},
        //        new WantedPerson { Id = 7, Name = "David Kim", Description = "Assault investigation", Type = "Felony"},
        //    };
        //}

        public void AddPerson(WantedPerson wantedPerson)
        {
            try
            {

                Init();
                if (wantedPerson == null)
                
                    throw new Exception("Invalid Persons Records");
                 //   conn.CreateTable<WantedPerson>();
                  
                    result = conn.Insert(wantedPerson);
                    StatusMessage = result == 0 ? "Insert Failed" : "Insert Succesful";

                    

                    Debug.WriteLine($"Name: {wantedPerson.Name}");
                    var count = conn.Table<WantedPerson>().Count();
                    Debug.WriteLine($"{StatusMessage}. WantedPerson table has {count} records.");

                

            }
            catch(Exception ex) {

                StatusMessage = "Failed to insert Data";
            
            }
        
        
        
        }



        public int DeletePerson(int id)
        {
            try
            {

                Init();
                if (conn == null)
                    throw new Exception("Database not initialized");

                result = conn.Table<WantedPerson>().Delete(q => q.Id == id);
                StatusMessage = result == 0 ? "Delete Failed" : "Delete Successful";
                return result;
            }
            catch
            {

                StatusMessage = "Failed to delete Data";
                return 0;
            }
        }


        public int UpdatePerson(WantedPerson person)
        {
            try
            {
                Init();
                if (conn == null)
                    throw new Exception("Database not initialized");

                // Update the record
                int result = conn.Update(person);

                StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to Update Data: {ex.Message}";
                return 0;
            }
        }




    }
}
