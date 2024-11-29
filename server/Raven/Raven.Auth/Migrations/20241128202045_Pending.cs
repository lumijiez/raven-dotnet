using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raven.Auth.Migrations
{
    /// <inheritdoc />
    public partial class Pending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetusertokens",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetusers",
                table: "aspnetusers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserroles",
                table: "aspnetuserroles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserlogins",
                table: "aspnetuserlogins");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserclaims",
                table: "aspnetuserclaims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetroles",
                table: "aspnetroles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetroleclaims",
                table: "aspnetroleclaims");

            migrationBuilder.RenameTable(
                name: "aspnetusertokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "aspnetusers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "aspnetuserroles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "aspnetuserlogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "aspnetuserclaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "aspnetroles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "aspnetroleclaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "AspNetUserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "AspNetUserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "twofactorenabled",
                table: "AspNetUsers",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "securitystamp",
                table: "AspNetUsers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phonenumberconfirmed",
                table: "AspNetUsers",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalizedusername",
                table: "AspNetUsers",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalizedemail",
                table: "AspNetUsers",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockoutend",
                table: "AspNetUsers",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockoutenabled",
                table: "AspNetUsers",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "isexternalaccountlinked",
                table: "AspNetUsers",
                newName: "IsExternalAccountLinked");

            migrationBuilder.RenameColumn(
                name: "googleid",
                table: "AspNetUsers",
                newName: "GoogleId");

            migrationBuilder.RenameColumn(
                name: "emailconfirmed",
                table: "AspNetUsers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "accessfailedcount",
                table: "AspNetUsers",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "AspNetUserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserroles_roleid",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "providerdisplayname",
                table: "AspNetUserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "providerkey",
                table: "AspNetUserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "AspNetUserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserlogins_userid",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "AspNetUserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "AspNetUserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetUserClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserclaims_userid",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "normalizedname",
                table: "AspNetRoles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "AspNetRoles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "AspNetRoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "AspNetRoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "AspNetRoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetRoleClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetroleclaims_roleid",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "aspnetusertokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "aspnetuserlogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "aspnetuserclaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "aspnetroles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "aspnetroleclaims");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "aspnetusertokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aspnetusertokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "aspnetusertokens",
                newName: "loginprovider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetusertokens",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "aspnetusers",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "aspnetusers",
                newName: "twofactorenabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "aspnetusers",
                newName: "securitystamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "aspnetusers",
                newName: "phonenumberconfirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "aspnetusers",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "aspnetusers",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "aspnetusers",
                newName: "normalizedusername");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "aspnetusers",
                newName: "normalizedemail");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "aspnetusers",
                newName: "lockoutend");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "aspnetusers",
                newName: "lockoutenabled");

            migrationBuilder.RenameColumn(
                name: "IsExternalAccountLinked",
                table: "aspnetusers",
                newName: "isexternalaccountlinked");

            migrationBuilder.RenameColumn(
                name: "GoogleId",
                table: "aspnetusers",
                newName: "googleid");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "aspnetusers",
                newName: "emailconfirmed");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "aspnetusers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "aspnetusers",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "aspnetusers",
                newName: "accessfailedcount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetusers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "aspnetuserroles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetuserroles",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "aspnetuserroles",
                newName: "IX_aspnetuserroles_roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetuserlogins",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "aspnetuserlogins",
                newName: "providerdisplayname");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "aspnetuserlogins",
                newName: "providerkey");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "aspnetuserlogins",
                newName: "loginprovider");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "aspnetuserlogins",
                newName: "IX_aspnetuserlogins_userid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetuserclaims",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "aspnetuserclaims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "aspnetuserclaims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetuserclaims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "aspnetuserclaims",
                newName: "IX_aspnetuserclaims_userid");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "aspnetroles",
                newName: "normalizedname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aspnetroles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "aspnetroles",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetroles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "aspnetroleclaims",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "aspnetroleclaims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "aspnetroleclaims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetroleclaims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "aspnetroleclaims",
                newName: "IX_aspnetroleclaims_roleid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetusertokens",
                table: "aspnetusertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetusers",
                table: "aspnetusers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserroles",
                table: "aspnetuserroles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserlogins",
                table: "aspnetuserlogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserclaims",
                table: "aspnetuserclaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetroles",
                table: "aspnetroles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetroleclaims",
                table: "aspnetroleclaims",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
