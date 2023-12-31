﻿namespace starter_app.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCanceledToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "isCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "isCanceled");
        }
    }
}
