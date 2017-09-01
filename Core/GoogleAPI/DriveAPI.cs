using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Core.GoogleAPI
{
	public static class DriveAPI
	{
		private static string[] Scopes = { DriveService.Scope.Drive };
		private static string ApplicationName = "Upload";

		public static string UploadImage(MemoryStream stream, string fileName)
		{
			var service = Init();

			return Upload(stream, service, fileName);
		}

		private static DriveService Init()
		{
			UserCredential credential;
			credential = GetCredentials();
			// Create Drive API service.
			var service = new DriveService(new BaseClientService.Initializer()

			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});
			
			return service;
		}

		private static string Upload(MemoryStream stream, DriveService service, string fileName)
		{
			var fileMetadata = new Google.Apis.Drive.v3.Data.File();
			fileMetadata.Name = fileName;
			fileMetadata.MimeType = "image/jpeg";
			fileMetadata.Parents = new List<string> { "0B4qC-p9go-NHbDV6NUUwUmtlVVE" };	
			FilesResource.CreateMediaUpload request;

			request = service.Files.Create(fileMetadata, stream, "image/jpeg");
			request.Fields = "id";
			request.Upload();

			var file = request.ResponseBody;

			return file.Id;
		}

		private static UserCredential GetCredentials()
		{
			UserCredential credential;
			var path = AppContext.BaseDirectory;
			path = path.Remove(path.IndexOf("Admin\\"));
			path += "Core\\GoogleAPI";
			using (var stream = new FileStream(path + "\\client_secret.json", FileMode.Open, FileAccess.Read))
			{
				string credPath = path;
				credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
			}

			return credential;
		}

		public static void DeleteFile(string fileId)
		{
			var service = Init();

			try
			{
				service.Files.Delete(fileId).Execute();
			}
			catch (Exception)
			{
			}
		}
	}
}
