using SQLite;

namespace XamarinForms.SQLite
{
    public class TodoItem
    {

        [PrimaryKey, AutoIncrement]
        public string ID { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; }

        public override string ToString()
        {
            return string.Format("Done : {0}, Text : {1}", Notes, Name);
        }
    }
}