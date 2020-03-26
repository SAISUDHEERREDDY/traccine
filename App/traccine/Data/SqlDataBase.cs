using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traccine.Models;

namespace traccine.Data
{
    public class SqlDataBase
    {
        public readonly SQLiteAsyncConnection _database;
        static object locker = new object();

        public SqlDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TimeLineModel>().Wait();

        }

        public async Task<int> AddTimeLineRecord(TimeLineModel data)
        {
            var Ack = await _database.InsertAsync(data);

            return Ack;

        }
        public async Task<List<TimeLineModel>> GetEventsbyDate(DateTime FromDate, DateTime ToDate)
        {

            var data = await _database.Table<TimeLineModel>()
                              .OrderByDescending(x => x.DateTime)
                              .ToListAsync();
            var finaldata = data.Where(i => i.DateTime.Date == ToDate.Date ).ToList();

            return finaldata;


        }
        public async Task<int> GetInteraction()
        {
            var toDate = DateTime.UtcNow;

            var data = await _database.Table<TimeLineModel>()
                              .OrderByDescending(x => x.DateTime)                              
                              .ToListAsync();
            var finaldata =  data.Where(i => i.DateTime.Date == toDate.Date).GroupBy(x => x.Email).Count();

            return finaldata;


        }
        public async Task<int> GetTotalInteractions()
        {

            var data = await _database.Table<TimeLineModel>()
                              .OrderByDescending(x => x.DateTime)
                              .ToListAsync();
            var finaldata = data.GroupBy(x => x.Email).Count();

            return finaldata;


        }
        public async Task<List<string>> GetTotalInteractedEmails()
        {

            var data = await _database.Table<TimeLineModel>()
                              .OrderByDescending(x => x.DateTime)
                              .ToListAsync();
            var finaldata = data.GroupBy(x => x.Email).Select(y=>y.Key);

            return finaldata.ToList();


        }
        public async Task<int> GetActiveHours()
        {
            var toDate = DateTime.UtcNow;

            var data = await _database.Table<TimeLineModel>()
                              .OrderByDescending(x => x.DateTime)
                              .ToListAsync();
                             
            var finaldata = data.Where(i => i.DateTime.Date == toDate.Date).GroupBy(x => x.DateTime.Hour).Count();

            return finaldata;


        }
        public async Task<int> GetTotalActiveHours()
        {

            var data = await _database.Table<TimeLineModel>()
                              .OrderByDescending(x => x.DateTime)
                              .ToListAsync();

            var finaldata = data.GroupBy(x => x.DateTime.Hour).Count();

            return finaldata;


        }
    }
}
