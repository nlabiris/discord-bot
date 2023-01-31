using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.DataAccess.Models {
    public class Job {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public string Description { get; set; }

        public int Interval { get; set; }
        public DateTime Created { get; set; }
    }
}
