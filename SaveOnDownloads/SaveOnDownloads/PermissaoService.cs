using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveOnDownloads
{
    public class PermissaoService
    {
        public async Task<bool> Permissao(params Permission[] permission)
        {
            var status = new List<PermissionStatus>();
            for (int i = 0; i < permission.Length; i++)
            {
                status.Add(await CrossPermissions.Current.CheckPermissionStatusAsync(permission[i]));
            }

            var results = default(Dictionary<Permission, PermissionStatus>);
            if (status.Any(x => x != PermissionStatus.Granted))
            {
                results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                //denied = results.Where(x => x.Value != PermissionStatus.Granted).ToList();
            }

            if (results == null || results.All(x => x.Value == PermissionStatus.Granted))
            {
                return true;
            }
            else
            {
                //var android = Device.RuntimePlatform == Device.Android;
                var exibeTelaErro = false;// !android;

                var rationale = true;

                foreach (var item in results)
                {
                    rationale = await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(item.Key);
                    if (rationale)
                        break;
                }

                if (!rationale)
                {
                    var permissoes = string.Join(", ", results.Select(x => x.Key.ToString()));
                    var mensagem = results.Count > 1
                         ? "Permissões necessárias, {0}"
                         : "Permissão necessária, {0}";

                    var retorno = CrossPermissions.Current.OpenAppSettings();

                }

                if (exibeTelaErro)
                {

                }
            }

            return false;
        }
    }
}
