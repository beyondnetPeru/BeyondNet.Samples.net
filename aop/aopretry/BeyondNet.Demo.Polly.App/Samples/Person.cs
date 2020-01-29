namespace BeyondNet.Demo.Polly.App.Samples
{
    public class FuncRepository
    {
        private string[] _table;

        public FuncRepository()
        {
            Load();
        }

        public string[] GetAll()
        {
            return _table;
        }

        public string GetIndex(int index)
        {
            return _table[index];
        }

        public string GetCriteria(int index, string name)
        {
            return _table[index] == name? _table[index]: "Do not exists!!!";
        }

        private void Load()
        {
            _table = new[] {"Alberto Arroyo", "Johana Sunohara", "Sergio Arroyo", "Aitana Arroyo"};
        }
    }

    public class Person
    {
        private readonly FuncRepository _repo;

        public Person()
        {
            _repo =  new FuncRepository();
        }

        public string[] GetAll()
        {
            return _repo.GetAll();
        }

        public string[] GetById(int id)
        {
            var record = _repo.GetIndex(id);
            return GetData(record);
        }

        public string[] GetByCriteria(int id, string name)
        {
            var record = _repo.GetCriteria(id, name);
            return GetData(record);
        }

        private static string[] GetData(string record)
        {
            var data = new[] {record};
            return data;
        }
    }
}
