namespace W2022A6NNB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trackaudio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Audio", c => c.Binary());
            AddColumn("dbo.Tracks", "AudioContentType", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "AudioContentType");
            DropColumn("dbo.Tracks", "Audio");
        }
    }
}
