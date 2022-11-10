
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpectruMLauncher
{
    public interface ILauncher //ПРОСТО ПОТОМУ ЧТО МОГУ И ХОЧУ
    {
        Command InitPlayer(string UserName, string UUID, string AccessToken);
    }
    internal class Launcher //: ILauncher Ну ладно, не буду
    {
        string MainPath;
        string command = "";
        string JVMArgs = "";
        public Launcher(string MainPath, string JVMArgs)
        {
            this.MainPath = MainPath;
            this.JVMArgs = JVMArgs;
        }
        public Command InitPlayer(string username, string uuid, string accesstoken)
        {
            Console.WriteLine(MainPath);
            Info.Args arguments = new Info.Args();
            Info.Values values = new Info.Values(MainPath);
            command = //Собираем комманду
            CollectHeapDump(arguments, values) +
            CollectOsName(arguments, values) +
            CollectOsVer(arguments, values) +
            CollectJavaLibPath(arguments, values) +
            CollectLauncherBrand(arguments, values) +
            CollectLauncherVer(arguments, values) +
            CollectCp(arguments, values) +
            CollectFabricMcEmu(arguments, values) +
            Username(arguments, username) +
            Version(arguments, values) +
            GameDir(arguments, values) +
            AssetsDir(arguments, values) +
            AssetIndex(arguments, values) +
            UUID(arguments, "\"" + uuid + "\"") +
            AccessToken(arguments, "\"" + accesstoken + "\"") +
            ClientID(arguments, "\"" + uuid + "\"") +
            //XUID(arguments, values) +
            UserType(arguments, values) +
            VersionType(arguments, values) + JVMArgs;//В JVMArgs в начале нужен пробел!!!
            
            return new Command(MainPath, command);
        }
        string CollectHeapDump(Info.Args args, Info.Values values)
        {
            return "-" + args.HeapDump + "=" + values.HeapDump + " ";
        }
        string CollectOsName(Info.Args args, Info.Values values)
        {
            return "-" + args.OsName + "=\"" + values.OsName + "\" ";
        }
        string CollectOsVer(Info.Args args, Info.Values values)
        {
            return "-" + args.OsVer + "=" + values.OsVer + " ";
        }
        string CollectJavaLibPath(Info.Args args, Info.Values values)
        {
            return "-" + args.JavaLibPath + "=" + values.JavaLibPath + " ";
        }
        string CollectLauncherBrand(Info.Args args, Info.Values values)
        {
            return "-" + args.LauncherBrand + "=" + values.LauncherBrand + " ";
        }
        string CollectLauncherVer(Info.Args args, Info.Values values)
        {
            return "-" + args.LauncherVer + "=" + values.LauncherVer + " ";
        }
        string CollectCp(Info.Args args, Info.Values values)
        {
            return "-" + args.cp + " " + values.cpPath + " ";
        }
        string CollectFabricMcEmu(Info.Args args, Info.Values values)
        {
            return "-" + args.FabricMcEmu + "=" + values.DFabricMcEmu + " ";
        }
        string Username(Info.Args args, string username)
        {
            return "--" + args.Username + " " + username + " ";
        }
        string Version(Info.Args args, Info.Values values)
        {
            return "--" + args.Version + " " + values.Version + " ";
        }
        string GameDir(Info.Args args, Info.Values values)
        {
            return "--" + args.GameDir + " " + values.GameDir + " ";
        }
        string AssetsDir(Info.Args args, Info.Values values)
        {
            return "--" + args.AssetsDir + " " + values.AssetsDir + " ";
        }
        string AssetIndex(Info.Args args, Info.Values values)
        {
            return "--" + args.AssetIndex + " " + values.AssetIndex + " ";
        }
        string UUID(Info.Args args, string uuid)
        {
            return "--" + args.UUID + " " + uuid + " ";
        }
        string AccessToken(Info.Args args, string access)
        {
            return "--" + args.AccessToken + " " + access + " ";
        }
        string ClientID(Info.Args args, string values)
        {
            return "--" + args.clientID + " " + values + " ";
        }
        string XUID(Info.Args args, Info.Values values)
        {
            return "--" + args.XUID + " " + values.XUID + " ";
        }
        string UserType(Info.Args args, Info.Values values)
        {
            return "--" + args.UserType + " " + values.UserType + " ";
        }
        string VersionType(Info.Args args, Info.Values values)
        {
            return "--" + args.VersionType + " " + values.VersionType + " ";
        }
    }
    public class Command
    {
        public Command(string MainPath, string command)
        {
            Runtime = MainPath + "\\runtime\\java-runtime-beta\\windows-x64\\java-runtime-beta\\bin\\java.exe";
            Args = command;
        }
        public string Runtime { get; }
        public string Args { get; }
    }
    public class Info
    {
        //static public string MainPath = "";
        static string ModelDriver = "Intel"; //Прыклад, Intel, AMD, Nvidia
        public class Args
        {
            //Пачатковыя аргументы
            public string HeapDump = "XX:HeapDumpPath";
            public string OsName = "Dos.name";
            public string OsVer = "Dos.version";
            public string JavaLibPath = "DJava.library.path";
            public string LauncherBrand = "Dminecraft.launcher.brand";
            public string LauncherVer = "Dminecraft.launcher.version";
            public string cp = "cp";
            //Канцовыя аргументы
            public string FabricMcEmu = "DFabricMcEmu";
            //Аргументы з дзвумя рісками
            public string Username = "username";
            public string Version = "version";
            public string GameDir = "gameDir";
            public string AssetsDir = "assetsDir";
            public string AssetIndex = "assetIndex";
            public string UUID = "uuid";
            public string AccessToken = "accessToken";
            public string clientID = "clientId";
            public string XUID = "xuid";
            public string UserType = "userType";
            public string VersionType = "versionType";
        }
        public class staticPaths
        {

        }
        public class Values
        {
            string MainPath;
            public Values(string MainPath)
            {
                this.MainPath = MainPath;
                RuntimePath = MainPath + "\\runtime\\java-runtime-beta\\windows-x64\\java-runtime-beta\\bin\\java.exe";
                JavaLibPath = MainPath + "\\versions\\fabric-loader-0.14.9-1.18.2\\natives";
                cpPath = MainPath + "\\libraries\\net\\fabricmc\\tiny-mappings-parser\\0.3.0+build.17\\tiny-mappings-parser-0.3.0+build.17.jar;" + MainPath + "\\libraries\\net\\fabricmc\\sponge-mixin\\0.11.4+mixin.0.8.5\\sponge-mixin-0.11.4+mixin.0.8.5.jar;" + MainPath + "\\libraries\\net\\fabricmc\\tiny-remapper\\0.8.2\\tiny-remapper-0.8.2.jar;" + MainPath + "\\libraries\\net\\fabricmc\\access-widener\\2.1.0\\access-widener-2.1.0.jar;" + MainPath + "\\libraries\\org\\ow2\\asm\\asm\\9.3\\asm-9.3.jar;" + MainPath + "\\libraries\\org\\ow2\\asm\\asm-analysis\\9.3\\asm-analysis-9.3.jar;" + MainPath + "\\libraries\\org\\ow2\\asm\\asm-commons\\9.3\\asm-commons-9.3.jar;" + MainPath + "\\libraries\\org\\ow2\\asm\\asm-tree\\9.3\\asm-tree-9.3.jar;" + MainPath + "\\libraries\\org\\ow2\\asm\\asm-util\\9.3\\asm-util-9.3.jar;" + MainPath + "\\libraries\\net\\fabricmc\\intermediary\\1.18.2\\intermediary-1.18.2.jar;" + MainPath + "\\libraries\\net\\fabricmc\\fabric-loader\\0.14.9\\fabric-loader-0.14.9.jar;" + MainPath + "\\libraries\\com\\mojang\\logging\\1.0.0\\logging-1.0.0.jar;" + MainPath + "\\libraries\\com\\mojang\\blocklist\\1.0.10\\blocklist-1.0.10.jar;" + MainPath + "\\libraries\\com\\mojang\\patchy\\2.2.10\\patchy-2.2.10.jar;" + MainPath + "\\libraries\\com\\github\\oshi\\oshi-core\\5.8.5\\oshi-core-5.8.5.jar;" + MainPath + "\\libraries\\net\\java\\dev\\jna\\jna\\5.10.0\\jna-5.10.0.jar;" + MainPath + "\\libraries\\net\\java\\dev\\jna\\jna-platform\\5.10.0\\jna-platform-5.10.0.jar;" + MainPath + "\\libraries\\org\\slf4j\\slf4j-api\\1.8.0-beta4\\slf4j-api-1.8.0-beta4.jar;" + MainPath + "\\libraries\\org\\apache\\logging\\log4j\\log4j-slf4j18-impl\\2.17.0\\log4j-slf4j18-impl-2.17.0.jar;" + MainPath + "\\libraries\\com\\ibm\\icu\\icu4j\\70.1\\icu4j-70.1.jar;" + MainPath + "\\libraries\\com\\mojang\\javabridge\\1.2.24\\javabridge-1.2.24.jar;" + MainPath + "\\libraries\\net\\sf\\jopt-simple\\jopt-simple\\5.0.4\\jopt-simple-5.0.4.jar;" + MainPath + "\\libraries\\io\\netty\\netty-all\\4.1.68.Final\\netty-all-4.1.68.Final.jar;" + MainPath + "\\libraries\\com\\google\\guava\\failureaccess\\1.0.1\\failureaccess-1.0.1.jar;" + MainPath + "\\libraries\\com\\google\\guava\\guava\\31.0.1-jre\\guava-31.0.1-jre.jar;" + MainPath + "\\libraries\\org\\apache\\commons\\commons-lang3\\3.12.0\\commons-lang3-3.12.0.jar;" + MainPath + "\\libraries\\commons-io\\commons-io\\2.11.0\\commons-io-2.11.0.jar;" + MainPath + "\\libraries\\commons-codec\\commons-codec\\1.15\\commons-codec-1.15.jar;" + MainPath + "\\libraries\\com\\mojang\\brigadier\\1.0.18\\brigadier-1.0.18.jar;" + MainPath + "\\libraries\\com\\mojang\\datafixerupper\\4.1.27\\datafixerupper-4.1.27.jar;" + MainPath + "\\libraries\\com\\google\\code\\gson\\gson\\2.8.9\\gson-2.8.9.jar;" + MainPath + "\\libraries\\com\\mojang\\authlib\\3.3.39\\authlib-3.3.39.jar;" + MainPath + "\\libraries\\org\\apache\\commons\\commons-compress\\1.21\\commons-compress-1.21.jar;" + MainPath + "\\libraries\\org\\apache\\httpcomponents\\httpclient\\4.5.13\\httpclient-4.5.13.jar;" + MainPath + "\\libraries\\commons-logging\\commons-logging\\1.2\\commons-logging-1.2.jar;" + MainPath + "\\libraries\\org\\apache\\httpcomponents\\httpcore\\4.4.14\\httpcore-4.4.14.jar;" + MainPath + "\\libraries\\it\\unimi\\dsi\\fastutil\\8.5.6\\fastutil-8.5.6.jar;" + MainPath + "\\libraries\\org\\apache\\logging\\log4j\\log4j-api\\2.17.0\\log4j-api-2.17.0.jar;" + MainPath + "\\libraries\\org\\apache\\logging\\log4j\\log4j-core\\2.17.0\\log4j-core-2.17.0.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl\\3.2.2\\lwjgl-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-jemalloc\\3.2.2\\lwjgl-jemalloc-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-openal\\3.2.2\\lwjgl-openal-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-opengl\\3.2.2\\lwjgl-opengl-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-glfw\\3.2.2\\lwjgl-glfw-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-stb\\3.2.2\\lwjgl-stb-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-tinyfd\\3.2.2\\lwjgl-tinyfd-3.2.2.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl\\3.2.2\\lwjgl-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl/3.2.2/lwjgl-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-jemalloc\\3.2.2\\lwjgl-jemalloc-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl-jemalloc/3.2.2/lwjgl-jemalloc-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-openal\\3.2.2\\lwjgl-openal-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl-openal/3.2.2/lwjgl-openal-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-opengl\\3.2.2\\lwjgl-opengl-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl-opengl/3.2.2/lwjgl-opengl-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-glfw\\3.2.2\\lwjgl-glfw-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl-glfw/3.2.2/lwjgl-glfw-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-tinyfd\\3.2.2\\lwjgl-tinyfd-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl-tinyfd/3.2.2/lwjgl-tinyfd-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\org\\lwjgl\\lwjgl-stb\\3.2.2\\lwjgl-stb-3.2.2.jar;" + MainPath + "\\libraries\\org/lwjgl/lwjgl-stb/3.2.2/lwjgl-stb-3.2.2-natives-windows.jar;" + MainPath + "\\libraries\\com\\mojang\\text2speech\\1.12.4\\text2speech-1.12.4.jar;" + MainPath + "\\libraries\\com\\mojang\\text2speech\\1.12.4\\text2speech-1.12.4.jar;" + MainPath + "\\libraries\\com/mojang/text2speech/1.12.4/text2speech-1.12.4-natives-windows.jar;" + MainPath + "\\versions\\fabric-loader-0.14.9-1.18.2\\fabric-loader-0.14.9-1.18.2.jar";
                GameDir = MainPath;
                AssetsDir = MainPath + "\\Assets";
            }
            public string HeapDump = "MojangTricks" + ModelDriver + "DriversForPerformance_javaw.exe_minecraft.exe.heapdump";
            public string OsName = "Windows 10"; //Прыклад
            public string OsVer = "10.0"; //Прыклад
            public string LauncherBrand = "IMALauncher"; //Прыклад
            public string LauncherVer = "1.0"; //Прыклад
            public string DFabricMcEmu = "net.minecraft.client.main.Main net.fabricmc.loader.impl.launch.knot.KnotClient";
            public string UserName = "TestPlayer"; //Прыклад
            public string Version = "fabric-loader-0.14.9-1.18.2"; //Прыклад?
            public string AssetIndex = "1.18"; //Прыклад?
            public string UUID = "0"; //Прыклад?
            public string AccessToken = "0"; //Прыклад?
            public string ClientID = "${clientid}"; //Прыклад?
            public string XUID = "${auth_xuid}"; //Прыклад?
            public string UserType = "mojang";
            public string VersionType = "release";
            public string RuntimePath;
            public string JavaLibPath;
            public string cpPath;
            public string GameDir;
            public string AssetsDir;

        }
    }
}
