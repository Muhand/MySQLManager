//This class is just a node for a new field, a field consists of 3 items
//1. The field name in the database, what is this field name in the database? (Columns name in the table of your db)
//2. The value of the field, what is the value you would like to enter for this new entry?
//3. Tag, what kind of field is this? is it a password? normal field? date? time? This helps to process the data when we create a new entry
//The enums class includes the valid tags
namespace Login
{
    public class NewField
    {
        public string fieldName
        {
            set;
            get;
        }
        public string fieldValue
        {
            set;
            get;
        }
        public enums.Tags tag
        {
            set;
            get;
        }
        public NewField(string fieldname, string fieldvlue, enums.Tags tg)
        {
            fieldName = fieldname;
            fieldValue = fieldvlue;
            tag = tg;
        }
    }
}
