/*
Kardinal.Net
Copyright(C) 2022 Marcelo O.Mendes


This program is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using Kardinal.Net.MediaTypes.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net
{
    /// <summary>
    /// Classe estática de tipos Mime de arquivos.
    /// </summary>
    public static class MediaTypeNames
    {

        private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
            {".323", "text/h323"},
            {".3g2", "video/3gpp2"},
            {".3gp", "video/3gpp"},
            {".3gp2", "video/3gpp2"},
            {".3gpp", "video/3gpp"},
            {".7z", "application/x-7z-compressed"},
            {".aa", "audio/audible"},
            {".AAC", "audio/aac"},
            {".aaf", "application/octet-stream"},
            {".aax", "audio/vnd.audible.aax"},
            {".ac3", "audio/ac3"},
            {".aca", "application/octet-stream"},
            {".accda", "application/msaccess.addin"},
            {".accdb", "application/msaccess"},
            {".accdc", "application/msaccess.cab"},
            {".accde", "application/msaccess"},
            {".accdr", "application/msaccess.runtime"},
            {".accdt", "application/msaccess"},
            {".accdw", "application/msaccess.webapplication"},
            {".accft", "application/msaccess.ftemplate"},
            {".acx", "application/internet-property-stream"},
            {".AddIn", "text/xml"},
            {".ade", "application/msaccess"},
            {".adobebridge", "application/x-bridge-url"},
            {".adp", "application/msaccess"},
            {".ADT", "audio/vnd.dlna.adts"},
            {".ADTS", "audio/aac"},
            {".afm", "application/octet-stream"},
            {".ai", "application/postscript"},
            {".aif", "audio/x-aiff"},
            {".aifc", "audio/aiff"},
            {".aiff", "audio/aiff"},
            {".air", "application/vnd.adobe.air-application-installer-package+zip"},
            {".amc", "application/x-mpeg"},
            {".application", "application/x-ms-application"},
            {".art", "image/x-jg"},
            {".asa", "application/xml"},
            {".asax", "application/xml"},
            {".ascx", "application/xml"},
            {".asd", "application/octet-stream"},
            {".asf", "video/x-ms-asf"},
            {".ashx", "application/xml"},
            {".asi", "application/octet-stream"},
            {".asm", "text/plain"},
            {".asmx", "application/xml"},
            {".aspx", "application/xml"},
            {".asr", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".atom", "application/atom+xml"},
            {".au", "audio/basic"},
            {".avi", "video/x-msvideo"},
            {".axs", "application/olescript"},
            {".bas", "text/plain"},
            {".bcpio", "application/x-bcpio"},
            {".bin", "application/octet-stream"},
            {".bmp", "image/bmp"},
            {".c", "text/plain"},
            {".cab", "application/octet-stream"},
            {".caf", "audio/x-caf"},
            {".calx", "application/vnd.ms-office.calx"},
            {".cat", "application/vnd.ms-pki.seccat"},
            {".cc", "text/plain"},
            {".cd", "text/plain"},
            {".cdda", "audio/aiff"},
            {".cdf", "application/x-cdf"},
            {".cer", "application/x-x509-ca-cert"},
            {".chm", "application/octet-stream"},
            {".class", "application/x-java-applet"},
            {".clp", "application/x-msclip"},
            {".cmx", "image/x-cmx"},
            {".cnf", "text/plain"},
            {".cod", "image/cis-cod"},
            {".config", "application/xml"},
            {".contact", "text/x-ms-contact"},
            {".coverage", "application/xml"},
            {".cpio", "application/x-cpio"},
            {".cpp", "text/plain"},
            {".crd", "application/x-mscardfile"},
            {".crl", "application/pkix-crl"},
            {".crt", "application/x-x509-ca-cert"},
            {".cs", "text/plain"},
            {".csdproj", "text/plain"},
            {".csh", "application/x-csh"},
            {".csproj", "text/plain"},
            {".css", "text/css"},
            {".csv", "text/csv"},
            {".cur", "application/octet-stream"},
            {".cxx", "text/plain"},
            {".dat", "application/octet-stream"},
            {".datasource", "application/xml"},
            {".dbproj", "text/plain"},
            {".dcr", "application/x-director"},
            {".def", "text/plain"},
            {".deploy", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dgml", "application/xml"},
            {".dib", "image/bmp"},
            {".dif", "video/x-dv"},
            {".dir", "application/x-director"},
            {".disco", "text/xml"},
            {".dll", "application/x-msdownload"},
            {".dll.config", "text/xml"},
            {".dlm", "text/dlm"},
            {".doc", "application/msword"},
            {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".dot", "application/msword"},
            {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
            {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {".dsp", "application/octet-stream"},
            {".dsw", "text/plain"},
            {".dtd", "text/xml"},
            {".dtsConfig", "text/xml"},
            {".dv", "video/x-dv"},
            {".dvi", "application/x-dvi"},
            {".dwf", "drawing/x-dwf"},
            {".dwp", "application/octet-stream"},
            {".dxr", "application/x-director"},
            {".eml", "message/rfc822"},
            {".emz", "application/octet-stream"},
            {".eot", "application/octet-stream"},
            {".eps", "application/postscript"},
            {".etl", "application/etl"},
            {".etx", "text/x-setext"},
            {".evy", "application/envoy"},
            {".exe", "application/octet-stream"},
            {".exe.config", "text/xml"},
            {".fdf", "application/vnd.fdf"},
            {".fif", "application/fractals"},
            {".filters", "Application/xml"},
            {".fla", "application/octet-stream"},
            {".flr", "x-world/x-vrml"},
            {".flv", "video/x-flv"},
            {".fsscript", "application/fsharp-script"},
            {".fsx", "application/fsharp-script"},
            {".generictest", "application/xml"},
            {".gif", "image/gif"},
            {".group", "text/x-ms-group"},
            {".gsm", "audio/x-gsm"},
            {".gtar", "application/x-gtar"},
            {".gz", "application/x-gzip"},
            {".h", "text/plain"},
            {".hdf", "application/x-hdf"},
            {".hdml", "text/x-hdml"},
            {".hhc", "application/x-oleobject"},
            {".hhk", "application/octet-stream"},
            {".hhp", "application/octet-stream"},
            {".hlp", "application/winhlp"},
            {".hpp", "text/plain"},
            {".hqx", "application/mac-binhex40"},
            {".hta", "application/hta"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".htt", "text/webviewhtml"},
            {".hxa", "application/xml"},
            {".hxc", "application/xml"},
            {".hxd", "application/octet-stream"},
            {".hxe", "application/xml"},
            {".hxf", "application/xml"},
            {".hxh", "application/octet-stream"},
            {".hxi", "application/octet-stream"},
            {".hxk", "application/xml"},
            {".hxq", "application/octet-stream"},
            {".hxr", "application/octet-stream"},
            {".hxs", "application/octet-stream"},
            {".hxt", "text/html"},
            {".hxv", "application/xml"},
            {".hxw", "application/octet-stream"},
            {".hxx", "text/plain"},
            {".i", "text/plain"},
            {".ico", "image/x-icon"},
            {".ics", "application/octet-stream"},
            {".idl", "text/plain"},
            {".ief", "image/ief"},
            {".iii", "application/x-iphone"},
            {".inc", "text/plain"},
            {".inf", "application/octet-stream"},
            {".inl", "text/plain"},
            {".ins", "application/x-internet-signup"},
            {".ipa", "application/x-itunes-ipa"},
            {".ipg", "application/x-itunes-ipg"},
            {".ipproj", "text/plain"},
            {".ipsw", "application/x-itunes-ipsw"},
            {".iqy", "text/x-ms-iqy"},
            {".isp", "application/x-internet-signup"},
            {".ite", "application/x-itunes-ite"},
            {".itlp", "application/x-itunes-itlp"},
            {".itms", "application/x-itunes-itms"},
            {".itpc", "application/x-itunes-itpc"},
            {".IVF", "video/x-ivf"},
            {".jar", "application/java-archive"},
            {".java", "application/octet-stream"},
            {".jck", "application/liquidmotion"},
            {".jcz", "application/liquidmotion"},
            {".jfif", "image/pjpeg"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpb", "application/octet-stream"},
            {".jpe", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".json", "application/json"},
            {".jsx", "text/jscript"},
            {".jsxbin", "text/plain"},
            {".latex", "application/x-latex"},
            {".library-ms", "application/windows-library+xml"},
            {".lit", "application/x-ms-reader"},
            {".loadtest", "application/xml"},
            {".lpk", "application/octet-stream"},
            {".lsf", "video/x-la-asf"},
            {".lst", "text/plain"},
            {".lsx", "video/x-la-asf"},
            {".lzh", "application/octet-stream"},
            {".m13", "application/x-msmediaview"},
            {".m14", "application/x-msmediaview"},
            {".m1v", "video/mpeg"},
            {".m2t", "video/vnd.dlna.mpeg-tts"},
            {".m2ts", "video/vnd.dlna.mpeg-tts"},
            {".m2v", "video/mpeg"},
            {".m3u", "audio/x-mpegurl"},
            {".m3u8", "audio/x-mpegurl"},
            {".m4a", "audio/m4a"},
            {".m4b", "audio/m4b"},
            {".m4p", "audio/m4p"},
            {".m4r", "audio/x-m4r"},
            {".m4v", "video/x-m4v"},
            {".mac", "image/x-macpaint"},
            {".mak", "text/plain"},
            {".man", "application/x-troff-man"},
            {".manifest", "application/x-ms-manifest"},
            {".map", "text/plain"},
            {".master", "application/xml"},
            {".mda", "application/msaccess"},
            {".mdb", "application/x-msaccess"},
            {".mde", "application/msaccess"},
            {".mdp", "application/octet-stream"},
            {".me", "application/x-troff-me"},
            {".mfp", "application/x-shockwave-flash"},
            {".mht", "message/rfc822"},
            {".mhtml", "message/rfc822"},
            {".mid", "audio/mid"},
            {".midi", "audio/mid"},
            {".mix", "application/octet-stream"},
            {".mk", "text/plain"},
            {".mmf", "application/x-smaf"},
            {".mno", "text/xml"},
            {".mny", "application/x-msmoney"},
            {".mod", "video/mpeg"},
            {".mov", "video/quicktime"},
            {".movie", "video/x-sgi-movie"},
            {".mp2", "video/mpeg"},
            {".mp2v", "video/mpeg"},
            {".mp3", "audio/mpeg"},
            {".mp4", "video/mp4"},
            {".mp4v", "video/mp4"},
            {".mpa", "video/mpeg"},
            {".mpe", "video/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpf", "application/vnd.ms-mediapackage"},
            {".mpg", "video/mpeg"},
            {".mpp", "application/vnd.ms-project"},
            {".mpv2", "video/mpeg"},
            {".mqv", "video/quicktime"},
            {".ms", "application/x-troff-ms"},
            {".msi", "application/octet-stream"},
            {".mso", "application/octet-stream"},
            {".mts", "video/vnd.dlna.mpeg-tts"},
            {".mtx", "application/xml"},
            {".mvb", "application/x-msmediaview"},
            {".mvc", "application/x-miva-compiled"},
            {".mxp", "application/x-mmxp"},
            {".nc", "application/x-netcdf"},
            {".nsc", "video/x-ms-asf"},
            {".nws", "message/rfc822"},
            {".ocx", "application/octet-stream"},
            {".oda", "application/oda"},
            {".odc", "text/x-ms-odc"},
            {".odh", "text/plain"},
            {".odl", "text/plain"},
            {".odp", "application/vnd.oasis.opendocument.presentation"},
            {".ods", "application/oleobject"},
            {".odt", "application/vnd.oasis.opendocument.text"},
            {".one", "application/onenote"},
            {".onea", "application/onenote"},
            {".onepkg", "application/onenote"},
            {".onetmp", "application/onenote"},
            {".onetoc", "application/onenote"},
            {".onetoc2", "application/onenote"},
            {".orderedtest", "application/xml"},
            {".osdx", "application/opensearchdescription+xml"},
            {".p10", "application/pkcs10"},
            {".p12", "application/x-pkcs12"},
            {".p7b", "application/x-pkcs7-certificates"},
            {".p7c", "application/pkcs7-mime"},
            {".p7m", "application/pkcs7-mime"},
            {".p7r", "application/x-pkcs7-certreqresp"},
            {".p7s", "application/pkcs7-signature"},
            {".pbm", "image/x-portable-bitmap"},
            {".pcast", "application/x-podcast"},
            {".pct", "image/pict"},
            {".pcx", "application/octet-stream"},
            {".pcz", "application/octet-stream"},
            {".pdf", "application/pdf"},
            {".pfb", "application/octet-stream"},
            {".pfm", "application/octet-stream"},
            {".pfx", "application/x-pkcs12"},
            {".pgm", "image/x-portable-graymap"},
            {".pic", "image/pict"},
            {".pict", "image/pict"},
            {".pkgdef", "text/plain"},
            {".pkgundef", "text/plain"},
            {".pko", "application/vnd.ms-pki.pko"},
            {".pls", "audio/scpls"},
            {".pma", "application/x-perfmon"},
            {".pmc", "application/x-perfmon"},
            {".pml", "application/x-perfmon"},
            {".pmr", "application/x-perfmon"},
            {".pmw", "application/x-perfmon"},
            {".png", "image/png"},
            {".pnm", "image/x-portable-anymap"},
            {".pnt", "image/x-macpaint"},
            {".pntg", "image/x-macpaint"},
            {".pnz", "image/png"},
            {".pot", "application/vnd.ms-powerpoint"},
            {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
            {".ppa", "application/vnd.ms-powerpoint"},
            {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {".ppm", "image/x-portable-pixmap"},
            {".pps", "application/vnd.ms-powerpoint"},
            {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {".ppt", "application/vnd.ms-powerpoint"},
            {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".prf", "application/pics-rules"},
            {".prm", "application/octet-stream"},
            {".prx", "application/octet-stream"},
            {".ps", "application/postscript"},
            {".psc1", "application/PowerShell"},
            {".psd", "application/octet-stream"},
            {".psess", "application/xml"},
            {".psm", "application/octet-stream"},
            {".psp", "application/octet-stream"},
            {".pub", "application/x-mspublisher"},
            {".pwz", "application/vnd.ms-powerpoint"},
            {".qht", "text/x-html-insertion"},
            {".qhtm", "text/x-html-insertion"},
            {".qt", "video/quicktime"},
            {".qti", "image/x-quicktime"},
            {".qtif", "image/x-quicktime"},
            {".qtl", "application/x-quicktimeplayer"},
            {".qxd", "application/octet-stream"},
            {".ra", "audio/x-pn-realaudio"},
            {".ram", "audio/x-pn-realaudio"},
            {".rar", "application/octet-stream"},
            {".ras", "image/x-cmu-raster"},
            {".rat", "application/rat-file"},
            {".rc", "text/plain"},
            {".rc2", "text/plain"},
            {".rct", "text/plain"},
            {".rdlc", "application/xml"},
            {".resx", "application/xml"},
            {".rf", "image/vnd.rn-realflash"},
            {".rgb", "image/x-rgb"},
            {".rgs", "text/plain"},
            {".rm", "application/vnd.rn-realmedia"},
            {".rmi", "audio/mid"},
            {".rmp", "application/vnd.rn-rn_music_package"},
            {".roff", "application/x-troff"},
            {".rpm", "audio/x-pn-realaudio-plugin"},
            {".rqy", "text/x-ms-rqy"},
            {".rtf", "application/rtf"},
            {".rtx", "text/richtext"},
            {".ruleset", "application/xml"},
            {".s", "text/plain"},
            {".safariextz", "application/x-safari-safariextz"},
            {".scd", "application/x-msschedule"},
            {".sct", "text/scriptlet"},
            {".sd2", "audio/x-sd2"},
            {".sdp", "application/sdp"},
            {".sea", "application/octet-stream"},
            {".searchConnector-ms", "application/windows-search-connector+xml"},
            {".setpay", "application/set-payment-initiation"},
            {".setreg", "application/set-registration-initiation"},
            {".settings", "application/xml"},
            {".sgimb", "application/x-sgimb"},
            {".sgml", "text/sgml"},
            {".sh", "application/x-sh"},
            {".shar", "application/x-shar"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".sitemap", "application/xml"},
            {".skin", "application/xml"},
            {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
            {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
            {".slk", "application/vnd.ms-excel"},
            {".sln", "text/plain"},
            {".slupkg-ms", "application/x-ms-license"},
            {".smd", "audio/x-smd"},
            {".smi", "application/octet-stream"},
            {".smx", "audio/x-smd"},
            {".smz", "audio/x-smd"},
            {".snd", "audio/basic"},
            {".snippet", "application/xml"},
            {".snp", "application/octet-stream"},
            {".sol", "text/plain"},
            {".sor", "text/plain"},
            {".spc", "application/x-pkcs7-certificates"},
            {".spl", "application/futuresplash"},
            {".src", "application/x-wais-source"},
            {".srf", "text/plain"},
            {".SSISDeploymentManifest", "text/xml"},
            {".ssm", "application/streamingmedia"},
            {".sst", "application/vnd.ms-pki.certstore"},
            {".stl", "application/vnd.ms-pki.stl"},
            {".sv4cpio", "application/x-sv4cpio"},
            {".sv4crc", "application/x-sv4crc"},
            {".svc", "application/xml"},
            {".swf", "application/x-shockwave-flash"},
            {".t", "application/x-troff"},
            {".tar", "application/x-tar"},
            {".tcl", "application/x-tcl"},
            {".testrunconfig", "application/xml"},
            {".testsettings", "application/xml"},
            {".tex", "application/x-tex"},
            {".texi", "application/x-texinfo"},
            {".texinfo", "application/x-texinfo"},
            {".tgz", "application/x-compressed"},
            {".thmx", "application/vnd.ms-officetheme"},
            {".thn", "application/octet-stream"},
            {".tif", "image/tiff"},
            {".tiff", "image/tiff"},
            {".tlh", "text/plain"},
            {".tli", "text/plain"},
            {".toc", "application/octet-stream"},
            {".tr", "application/x-troff"},
            {".trm", "application/x-msterminal"},
            {".trx", "application/xml"},
            {".ts", "video/vnd.dlna.mpeg-tts"},
            {".tsv", "text/tab-separated-values"},
            {".ttf", "application/octet-stream"},
            {".tts", "video/vnd.dlna.mpeg-tts"},
            {".txt", "text/plain"},
            {".u32", "application/octet-stream"},
            {".uls", "text/iuls"},
            {".user", "text/plain"},
            {".ustar", "application/x-ustar"},
            {".vb", "text/plain"},
            {".vbdproj", "text/plain"},
            {".vbk", "video/mpeg"},
            {".vbproj", "text/plain"},
            {".vbs", "text/vbscript"},
            {".vcf", "text/x-vcard"},
            {".vcproj", "Application/xml"},
            {".vcs", "text/plain"},
            {".vcxproj", "Application/xml"},
            {".vddproj", "text/plain"},
            {".vdp", "text/plain"},
            {".vdproj", "text/plain"},
            {".vdx", "application/vnd.ms-visio.viewer"},
            {".vml", "text/xml"},
            {".vscontent", "application/xml"},
            {".vsct", "text/xml"},
            {".vsd", "application/vnd.visio"},
            {".vsi", "application/ms-vsi"},
            {".vsix", "application/vsix"},
            {".vsixlangpack", "text/xml"},
            {".vsixmanifest", "text/xml"},
            {".vsmdi", "application/xml"},
            {".vspscc", "text/plain"},
            {".vss", "application/vnd.visio"},
            {".vsscc", "text/plain"},
            {".vssettings", "text/xml"},
            {".vssscc", "text/plain"},
            {".vst", "application/vnd.visio"},
            {".vstemplate", "text/xml"},
            {".vsto", "application/x-ms-vsto"},
            {".vsw", "application/vnd.visio"},
            {".vsx", "application/vnd.visio"},
            {".vtx", "application/vnd.visio"},
            {".wav", "audio/wav"},
            {".wave", "audio/wav"},
            {".wax", "audio/x-ms-wax"},
            {".wbk", "application/msword"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wcm", "application/vnd.ms-works"},
            {".wdb", "application/vnd.ms-works"},
            {".wdp", "image/vnd.ms-photo"},
            {".webarchive", "application/x-safari-webarchive"},
            {".webtest", "application/xml"},
            {".wiq", "application/xml"},
            {".wiz", "application/msword"},
            {".wks", "application/vnd.ms-works"},
            {".WLMP", "application/wlmoviemaker"},
            {".wlpginstall", "application/x-wlpg-detect"},
            {".wlpginstall3", "application/x-wlpg3-detect"},
            {".wm", "video/x-ms-wm"},
            {".wma", "audio/x-ms-wma"},
            {".wmd", "application/x-ms-wmd"},
            {".wmf", "application/x-msmetafile"},
            {".wml", "text/vnd.wap.wml"},
            {".wmlc", "application/vnd.wap.wmlc"},
            {".wmls", "text/vnd.wap.wmlscript"},
            {".wmlsc", "application/vnd.wap.wmlscriptc"},
            {".wmp", "video/x-ms-wmp"},
            {".wmv", "video/x-ms-wmv"},
            {".wmx", "video/x-ms-wmx"},
            {".wmz", "application/x-ms-wmz"},
            {".wpl", "application/vnd.ms-wpl"},
            {".wps", "application/vnd.ms-works"},
            {".wri", "application/x-mswrite"},
            {".wrl", "x-world/x-vrml"},
            {".wrz", "x-world/x-vrml"},
            {".wsc", "text/scriptlet"},
            {".wsdl", "text/xml"},
            {".wvx", "video/x-ms-wvx"},
            {".x", "application/directx"},
            {".xaf", "x-world/x-vrml"},
            {".xaml", "application/xaml+xml"},
            {".xap", "application/x-silverlight-app"},
            {".xbap", "application/x-ms-xbap"},
            {".xbm", "image/x-xbitmap"},
            {".xdr", "text/plain"},
            {".xht", "application/xhtml+xml"},
            {".xhtml", "application/xhtml+xml"},
            {".xla", "application/vnd.ms-excel"},
            {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
            {".xlc", "application/vnd.ms-excel"},
            {".xld", "application/vnd.ms-excel"},
            {".xlk", "application/vnd.ms-excel"},
            {".xll", "application/vnd.ms-excel"},
            {".xlm", "application/vnd.ms-excel"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".xlt", "application/vnd.ms-excel"},
            {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
            {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {".xlw", "application/vnd.ms-excel"},
            {".xml", "text/xml"},
            {".xmta", "application/xml"},
            {".xof", "x-world/x-vrml"},
            {".XOML", "text/plain"},
            {".xpm", "image/x-xpixmap"},
            {".xps", "application/vnd.ms-xpsdocument"},
            {".xrm-ms", "text/xml"},
            {".xsc", "application/xml"},
            {".xsd", "text/xml"},
            {".xsf", "text/xml"},
            {".xsl", "text/xml"},
            {".xslt", "text/xml"},
            {".xsn", "application/octet-stream"},
            {".xss", "application/xml"},
            {".xtp", "application/octet-stream"},
            {".xwd", "image/x-xwindowdump"},
            {".z", "application/x-compress"},
            {".zip", "application/x-zip-compressed"}
        };

        /// <summary>
        /// Método público para obtenção de tipo mime com base na extensão do arquivo.
        /// </summary>
        /// <param name="extension">Extensão do arquivo.</param>
        /// <returns>Tipo Mime do arquivo.</returns>
        public static string GetMediaType([NotNull] string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(Resource.ERROR_EXTENSION_INVALID);
            }

            if (!extension.StartsWith("."))
            {
                extension = $".{extension}";
            }

            return _mappings.TryGetValue(extension, out string mime) ? mime : Application.OCTET_STREAM;
        }

        /// <summary>
        /// Mimetypes do tipo Application.
        /// </summary>
        public static class Application
        {
            /// <summary>
            /// Declaração de 7z.
            /// </summary>
            public const string SEVENZIP = "application/x-7z-compressed";

            /// <summary>
            /// Declaração de aaf.
            /// </summary>
            public const string AAF = "application/octet-stream";

            /// <summary>
            /// Declaração de aca.
            /// </summary>
            public const string ACA = "application/octet-stream";

            /// <summary>
            /// Declaração de accda.
            /// </summary>
            public const string ACCDA = "application/msaccess.addin";

            /// <summary>
            /// Declaração de accdb.
            /// </summary>
            public const string ACCDB = "application/msaccess";

            /// <summary>
            /// Declaração de accdc.
            /// </summary>
            public const string ACCDC = "application/msaccess.cab";

            /// <summary>
            /// Declaração de accde.
            /// </summary>
            public const string ACCDE = "application/msaccess";

            /// <summary>
            /// Declaração de accdr.
            /// </summary>
            public const string ACCDR = "application/msaccess.runtime";

            /// <summary>
            /// Declaração de accdt.
            /// </summary>
            public const string ACCDT = "application/msaccess";

            /// <summary>
            /// Declaração de accdw.
            /// </summary>
            public const string ACCDW = "application/msaccess.webapplication";

            /// <summary>
            /// Declaração de accft.
            /// </summary>
            public const string ACCFT = "application/msaccess.ftemplate";

            /// <summary>
            /// Declaração de acx.
            /// </summary>
            public const string ACX = "application/internet-property-stream";

            /// <summary>
            /// Declaração de ade.
            /// </summary>
            public const string ADE = "application/msaccess";

            /// <summary>
            /// Declaração de adobebridge.
            /// </summary>
            public const string ADOBEBRIDGE = "application/x-bridge-url";

            /// <summary>
            /// Declaração de adp.
            /// </summary>
            public const string ADP = "application/msaccess";

            /// <summary>
            /// Declaração de afm.
            /// </summary>
            public const string AFM = "application/octet-stream";

            /// <summary>
            /// Declaração de ai.
            /// </summary>
            public const string AI = "application/postscript";

            /// <summary>
            /// Declaração de air.
            /// </summary>
            public const string AIR = "application/vnd.adobe.air-application-installer-package+zip";

            /// <summary>
            /// Declaração de amc.
            /// </summary>
            public const string AMC = "application/x-mpeg";

            /// <summary>
            /// Declaração de application.
            /// </summary>
            public const string APPLICATION = "application/x-ms-application";

            /// <summary>
            /// Declaração de asa.
            /// </summary>
            public const string ASA = "application/xml";

            /// <summary>
            /// Declaração de asax.
            /// </summary>
            public const string ASAX = "application/xml";

            /// <summary>
            /// Declaração de ascx.
            /// </summary>
            public const string ASCX = "application/xml";

            /// <summary>
            /// Declaração de asd.
            /// </summary>
            public const string ASD = "application/octet-stream";

            /// <summary>
            /// Declaração de ashx.
            /// </summary>
            public const string ASHX = "application/xml";

            /// <summary>
            /// Declaração de asi.
            /// </summary>
            public const string ASI = "application/octet-stream";

            /// <summary>
            /// Declaração de asmx.
            /// </summary>
            public const string ASMX = "application/xml";

            /// <summary>
            /// Declaração de aspx.
            /// </summary>
            public const string ASPX = "application/xml";

            /// <summary>
            /// Declaração de atom.
            /// </summary>
            public const string ATOM = "application/atom+xml";

            /// <summary>
            /// Declaração de axs.
            /// </summary>
            public const string AXS = "application/olescript";

            /// <summary>
            /// Declaração de bcpio.
            /// </summary>
            public const string BCPIO = "application/x-bcpio";

            /// <summary>
            /// Declaração de bin.
            /// </summary>
            public const string BIN = "application/octet-stream";

            /// <summary>
            /// Declaração de cab.
            /// </summary>
            public const string CAB = "application/octet-stream";

            /// <summary>
            /// Declaração de calx.
            /// </summary>
            public const string CALX = "application/vnd.ms-office.calx";

            /// <summary>
            /// Declaração de cat.
            /// </summary>
            public const string CAT = "application/vnd.ms-pki.seccat";

            /// <summary>
            /// Declaração de cdf.
            /// </summary>
            public const string CDF = "application/x-cdf";

            /// <summary>
            /// Declaração de cer.
            /// </summary>
            public const string CER = "application/x-x509-ca-cert";

            /// <summary>
            /// Declaração de chm.
            /// </summary>
            public const string CHM = "application/octet-stream";

            /// <summary>
            /// Declaração de class.
            /// </summary>
            public const string CLASS = "application/x-java-applet";

            /// <summary>
            /// Declaração de clp.
            /// </summary>
            public const string CLP = "application/x-msclip";

            /// <summary>
            /// Declaração de config.
            /// </summary>
            public const string CONFIG = "application/xml";

            /// <summary>
            /// Declaração de coverage.
            /// </summary>
            public const string COVERAGE = "application/xml";

            /// <summary>
            /// Declaração de cpio.
            /// </summary>
            public const string CPIO = "application/x-cpio";

            /// <summary>
            /// Declaração de crd.
            /// </summary>
            public const string CRD = "application/x-mscardfile";

            /// <summary>
            /// Declaração de crl.
            /// </summary>
            public const string CRL = "application/pkix-crl";

            /// <summary>
            /// Declaração de crt.
            /// </summary>
            public const string CRT = "application/x-x509-ca-cert";

            /// <summary>
            /// Declaração de csh.
            /// </summary>
            public const string CSH = "application/x-csh";

            /// <summary>
            /// Declaração de cur.
            /// </summary>
            public const string CUR = "application/octet-stream";

            /// <summary>
            /// Declaração de dat.
            /// </summary>
            public const string DAT = "application/octet-stream";

            /// <summary>
            /// Declaração de datasource.
            /// </summary>
            public const string DATASOURCE = "application/xml";

            /// <summary>
            /// Declaração de dcr.
            /// </summary>
            public const string DCR = "application/x-director";

            /// <summary>
            /// Declaração de deploy.
            /// </summary>
            public const string DEPLOY = "application/octet-stream";

            /// <summary>
            /// Declaração de der.
            /// </summary>
            public const string DER = "application/x-x509-ca-cert";

            /// <summary>
            /// Declaração de dgml.
            /// </summary>
            public const string DGML = "application/xml";

            /// <summary>
            /// Declaração de dir.
            /// </summary>
            public const string DIR = "application/x-director";

            /// <summary>
            /// Declaração de dll.
            /// </summary>
            public const string DLL = "application/x-msdownload";

            /// <summary>
            /// Declaração de doc.
            /// </summary>
            public const string DOC = "application/msword";

            /// <summary>
            /// Declaração de docm.
            /// </summary>
            public const string DOCM = "application/vnd.ms-word.document.macroenabled.12";

            /// <summary>
            /// Declaração de docx.
            /// </summary>
            public const string DOCX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            /// <summary>
            /// Declaração de dot.
            /// </summary>
            public const string DOT = "application/msword";

            /// <summary>
            /// Declaração de dotm.
            /// </summary>
            public const string DOTM = "application/vnd.ms-word.template.macroenabled.12";

            /// <summary>
            /// Declaração de dotx.
            /// </summary>
            public const string DOTX = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";

            /// <summary>
            /// Declaração de dsp.
            /// </summary>
            public const string DSP = "application/octet-stream";

            /// <summary>
            /// Declaração de dvi.
            /// </summary>
            public const string DVI = "application/x-dvi";

            /// <summary>
            /// Declaração de dwp.
            /// </summary>
            public const string DWP = "application/octet-stream";

            /// <summary>
            /// Declaração de dxr.
            /// </summary>
            public const string DXR = "application/x-director";

            /// <summary>
            /// Declaração de emz.
            /// </summary>
            public const string EMZ = "application/octet-stream";

            /// <summary>
            /// Declaração de eot.
            /// </summary>
            public const string EOT = "application/octet-stream";

            /// <summary>
            /// Declaração de eps.
            /// </summary>
            public const string EPS = "application/postscript";

            /// <summary>
            /// Declaração de etl.
            /// </summary>
            public const string ETL = "application/etl";

            /// <summary>
            /// Declaração de evy.
            /// </summary>
            public const string EVY = "application/envoy";

            /// <summary>
            /// Declaração de exe.
            /// </summary>
            public const string EXE = "application/octet-stream";

            /// <summary>
            /// Declaração de fdf.
            /// </summary>
            public const string FDF = "application/vnd.fdf";

            /// <summary>
            /// Declaração de fif.
            /// </summary>
            public const string FIF = "application/fractals";

            /// <summary>
            /// Declaração de fla.
            /// </summary>
            public const string FLA = "application/octet-stream";

            /// <summary>
            /// Declaração de fsscript.
            /// </summary>
            public const string FSSCRIPT = "application/fsharp-script";

            /// <summary>
            /// Declaração de fsx.
            /// </summary>
            public const string FSX = "application/fsharp-script";

            /// <summary>
            /// Declaração de generictest.
            /// </summary>
            public const string GENERICTEST = "application/xml";

            /// <summary>
            /// Declaração de gtar.
            /// </summary>
            public const string GTAR = "application/x-gtar";

            /// <summary>
            /// Declaração de gz.
            /// </summary>
            public const string GZ = "application/x-gzip";

            /// <summary>
            /// Declaração de hdf.
            /// </summary>
            public const string HDF = "application/x-hdf";

            /// <summary>
            /// Declaração de hhc.
            /// </summary>
            public const string HHC = "application/x-oleobject";

            /// <summary>
            /// Declaração de hhk.
            /// </summary>
            public const string HHK = "application/octet-stream";

            /// <summary>
            /// Declaração de hhp.
            /// </summary>
            public const string HHP = "application/octet-stream";

            /// <summary>
            /// Declaração de hlp.
            /// </summary>
            public const string HLP = "application/winhlp";

            /// <summary>
            /// Declaração de hqx.
            /// </summary>
            public const string HQX = "application/mac-binhex40";

            /// <summary>
            /// Declaração de hta.
            /// </summary>
            public const string HTA = "application/hta";

            /// <summary>
            /// Declaração de hxa.
            /// </summary>
            public const string HXA = "application/xml";

            /// <summary>
            /// Declaração de hxc.
            /// </summary>
            public const string HXC = "application/xml";

            /// <summary>
            /// Declaração de hxd.
            /// </summary>
            public const string HXD = "application/octet-stream";

            /// <summary>
            /// Declaração de hxe.
            /// </summary>
            public const string HXE = "application/xml";

            /// <summary>
            /// Declaração de hxf.
            /// </summary>
            public const string HXF = "application/xml";

            /// <summary>
            /// Declaração de hxh.
            /// </summary>
            public const string HXH = "application/octet-stream";

            /// <summary>
            /// Declaração de hxi.
            /// </summary>
            public const string HXI = "application/octet-stream";

            /// <summary>
            /// Declaração de hxk.
            /// </summary>
            public const string HXK = "application/xml";

            /// <summary>
            /// Declaração de hxq.
            /// </summary>
            public const string HXQ = "application/octet-stream";

            /// <summary>
            /// Declaração de hxr.
            /// </summary>
            public const string HXR = "application/octet-stream";

            /// <summary>
            /// Declaração de hxs.
            /// </summary>
            public const string HXS = "application/octet-stream";

            /// <summary>
            /// Declaração de hxv.
            /// </summary>
            public const string HXV = "application/xml";

            /// <summary>
            /// Declaração de hxw.
            /// </summary>
            public const string HXW = "application/octet-stream";

            /// <summary>
            /// Declaração de ics.
            /// </summary>
            public const string ICS = "application/octet-stream";

            /// <summary>
            /// Declaração de iii.
            /// </summary>
            public const string III = "application/x-iphone";

            /// <summary>
            /// Declaração de inf.
            /// </summary>
            public const string INF = "application/octet-stream";

            /// <summary>
            /// Declaração de ins.
            /// </summary>
            public const string INS = "application/x-internet-signup";

            /// <summary>
            /// Declaração de ipa.
            /// </summary>
            public const string IPA = "application/x-itunes-ipa";

            /// <summary>
            /// Declaração de ipg.
            /// </summary>
            public const string IPG = "application/x-itunes-ipg";

            /// <summary>
            /// Declaração de ipsw.
            /// </summary>
            public const string IPSW = "application/x-itunes-ipsw";

            /// <summary>
            /// Declaração de isp.
            /// </summary>
            public const string ISP = "application/x-internet-signup";

            /// <summary>
            /// Declaração de ite.
            /// </summary>
            public const string ITE = "application/x-itunes-ite";

            /// <summary>
            /// Declaração de itlp.
            /// </summary>
            public const string ITLP = "application/x-itunes-itlp";

            /// <summary>
            /// Declaração de itms.
            /// </summary>
            public const string ITMS = "application/x-itunes-itms";

            /// <summary>
            /// Declaração de itpc.
            /// </summary>
            public const string ITPC = "application/x-itunes-itpc";

            /// <summary>
            /// Declaração de jar.
            /// </summary>
            public const string JAR = "application/java-archive";

            /// <summary>
            /// Declaração de java.
            /// </summary>
            public const string JAVA = "application/octet-stream";

            /// <summary>
            /// Declaração de jck.
            /// </summary>
            public const string JCK = "application/liquidmotion";

            /// <summary>
            /// Declaração de jcz.
            /// </summary>
            public const string JCZ = "application/liquidmotion";

            /// <summary>
            /// Declaração de jnlp.
            /// </summary>
            public const string JNLP = "application/x-java-jnlp-file";

            /// <summary>
            /// Declaração de jpb.
            /// </summary>
            public const string JPB = "application/octet-stream";

            /// <summary>
            /// Declaração de js.
            /// </summary>
            public const string JS = "application/x-javascript";

            /// <summary>
            /// Declaração de json.
            /// </summary>
            public const string JSON = "application/json";

            /// <summary>
            /// Declaração de latex.
            /// </summary>
            public const string LATEX = "application/x-latex";

            /// <summary>
            /// Declaração de library-ms.
            /// </summary>
            public const string LIBRARY_MS = "application/windows-library+xml";

            /// <summary>
            /// Declaração de lit.
            /// </summary>
            public const string LIT = "application/x-ms-reader";

            /// <summary>
            /// Declaração de loadtest.
            /// </summary>
            public const string LOADTEST = "application/xml";

            /// <summary>
            /// Declaração de lpk.
            /// </summary>
            public const string LPK = "application/octet-stream";

            /// <summary>
            /// Declaração de lzh.
            /// </summary>
            public const string LZH = "application/octet-stream";

            /// <summary>
            /// Declaração de m13.
            /// </summary>
            public const string M13 = "application/x-msmediaview";

            /// <summary>
            /// Declaração de m14.
            /// </summary>
            public const string M14 = "application/x-msmediaview";

            /// <summary>
            /// Declaração de man.
            /// </summary>
            public const string MAN = "application/x-troff-man";

            /// <summary>
            /// Declaração de manifest.
            /// </summary>
            public const string MANIFEST = "application/x-ms-manifest";

            /// <summary>
            /// Declaração de master.
            /// </summary>
            public const string MASTER = "application/xml";

            /// <summary>
            /// Declaração de mda.
            /// </summary>
            public const string MDA = "application/msaccess";

            /// <summary>
            /// Declaração de mdb.
            /// </summary>
            public const string MDB = "application/x-msaccess";

            /// <summary>
            /// Declaração de mde.
            /// </summary>
            public const string MDE = "application/msaccess";

            /// <summary>
            /// Declaração de mdp.
            /// </summary>
            public const string MDP = "application/octet-stream";

            /// <summary>
            /// Declaração de me.
            /// </summary>
            public const string ME = "application/x-troff-me";

            /// <summary>
            /// Declaração de mfp.
            /// </summary>
            public const string MFP = "application/x-shockwave-flash";

            /// <summary>
            /// Declaração de mix.
            /// </summary>
            public const string MIX = "application/octet-stream";

            /// <summary>
            /// Declaração de mmf.
            /// </summary>
            public const string MMF = "application/x-smaf";

            /// <summary>
            /// Declaração de mny.
            /// </summary>
            public const string MNY = "application/x-msmoney";

            /// <summary>
            /// Declaração de mpf.
            /// </summary>
            public const string MPF = "application/vnd.ms-mediapackage";

            /// <summary>
            /// Declaração de mpp.
            /// </summary>
            public const string MPP = "application/vnd.ms-project";

            /// <summary>
            /// Declaração de ms.
            /// </summary>
            public const string MS = "application/x-troff-ms";

            /// <summary>
            /// Declaração de msi.
            /// </summary>
            public const string MSI = "application/octet-stream";

            /// <summary>
            /// Declaração de mso.
            /// </summary>
            public const string MSO = "application/octet-stream";

            /// <summary>
            /// Declaração de mtx.
            /// </summary>
            public const string MTX = "application/xml";

            /// <summary>
            /// Declaração de mvb.
            /// </summary>
            public const string MVB = "application/x-msmediaview";

            /// <summary>
            /// Declaração de mvc.
            /// </summary>
            public const string MVC = "application/x-miva-compiled";

            /// <summary>
            /// Declaração de mxp.
            /// </summary>
            public const string MXP = "application/x-mmxp";

            /// <summary>
            /// Declaração de nc.
            /// </summary>
            public const string NC = "application/x-netcdf";

            /// <summary>
            /// Declaração de tipo padrão de dados.
            /// </summary>
            public const string OCTET_STREAM = "application/octet-stream";

            /// <summary>
            /// Declaração de ocx.
            /// </summary>
            public const string OCX = "application/octet-stream";

            /// <summary>
            /// Declaração de oda.
            /// </summary>
            public const string ODA = "application/oda";

            /// <summary>
            /// Declaração de odp.
            /// </summary>
            public const string ODP = "application/vnd.oasis.opendocument.presentation";

            /// <summary>
            /// Declaração de ods.
            /// </summary>
            public const string ODS = "application/oleobject";

            /// <summary>
            /// Declaração de odt.
            /// </summary>
            public const string ODT = "application/vnd.oasis.opendocument.text";

            /// <summary>
            /// Declaração de one.
            /// </summary>
            public const string ONE = "application/onenote";

            /// <summary>
            /// Declaração de onea.
            /// </summary>
            public const string ONEA = "application/onenote";

            /// <summary>
            /// Declaração de onepkg.
            /// </summary>
            public const string ONEPKG = "application/onenote";

            /// <summary>
            /// Declaração de onetmp.
            /// </summary>
            public const string ONETMP = "application/onenote";

            /// <summary>
            /// Declaração de onetoc.
            /// </summary>
            public const string ONETOC = "application/onenote";

            /// <summary>
            /// Declaração de onetoc2.
            /// </summary>
            public const string ONETOC2 = "application/onenote";

            /// <summary>
            /// Declaração de orderedtest.
            /// </summary>
            public const string ORDEREDTEST = "application/xml";

            /// <summary>
            /// Declaração de osdx.
            /// </summary>
            public const string OSDX = "application/opensearchdescription+xml";

            /// <summary>
            /// Declaração de p10.
            /// </summary>
            public const string P10 = "application/pkcs10";

            /// <summary>
            /// Declaração de p12.
            /// </summary>
            public const string P12 = "application/x-pkcs12";

            /// <summary>
            /// Declaração de p7b.
            /// </summary>
            public const string P7B = "application/x-pkcs7-certificates";

            /// <summary>
            /// Declaração de p7c.
            /// </summary>
            public const string P7C = "application/pkcs7-mime";

            /// <summary>
            /// Declaração de p7m.
            /// </summary>
            public const string P7M = "application/pkcs7-mime";

            /// <summary>
            /// Declaração de p7r.
            /// </summary>
            public const string P7R = "application/x-pkcs7-certreqresp";

            /// <summary>
            /// Declaração de p7s.
            /// </summary>
            public const string P7S = "application/pkcs7-signature";

            /// <summary>
            /// Declaração de pcast.
            /// </summary>
            public const string PCAST = "application/x-podcast";

            /// <summary>
            /// Declaração de pcx.
            /// </summary>
            public const string PCX = "application/octet-stream";

            /// <summary>
            /// Declaração de pcz.
            /// </summary>
            public const string PCZ = "application/octet-stream";

            /// <summary>
            /// Declaração de pdf.
            /// </summary>
            public const string PDF = "application/pdf";

            /// <summary>
            /// Declaração de pfb.
            /// </summary>
            public const string PFB = "application/octet-stream";

            /// <summary>
            /// Declaração de pfm.
            /// </summary>
            public const string PFM = "application/octet-stream";

            /// <summary>
            /// Declaração de pfx.
            /// </summary>
            public const string PFX = "application/x-pkcs12";

            /// <summary>
            /// Declaração de pko.
            /// </summary>
            public const string PKO = "application/vnd.ms-pki.pko";

            /// <summary>
            /// Declaração de pma.
            /// </summary>
            public const string PMA = "application/x-perfmon";

            /// <summary>
            /// Declaração de pmc.
            /// </summary>
            public const string PMC = "application/x-perfmon";

            /// <summary>
            /// Declaração de pml.
            /// </summary>
            public const string PML = "application/x-perfmon";

            /// <summary>
            /// Declaração de pmr.
            /// </summary>
            public const string PMR = "application/x-perfmon";

            /// <summary>
            /// Declaração de pmw.
            /// </summary>
            public const string PMW = "application/x-perfmon";

            /// <summary>
            /// Declaração de pot.
            /// </summary>
            public const string POT = "application/vnd.ms-powerpoint";

            /// <summary>
            /// Declaração de potm.
            /// </summary>
            public const string POTM = "application/vnd.ms-powerpoint.template.macroenabled.12";

            /// <summary>
            /// Declaração de potx.
            /// </summary>
            public const string POTX = "application/vnd.openxmlformats-officedocument.presentationml.template";

            /// <summary>
            /// Declaração de ppa.
            /// </summary>
            public const string PPA = "application/vnd.ms-powerpoint";

            /// <summary>
            /// Declaração de ppam.
            /// </summary>
            public const string PPAM = "application/vnd.ms-powerpoint.addin.macroenabled.12";

            /// <summary>
            /// Declaração de pps.
            /// </summary>
            public const string PPS = "application/vnd.ms-powerpoint";

            /// <summary>
            /// Declaração de ppsm.
            /// </summary>
            public const string PPSM = "application/vnd.ms-powerpoint.slideshow.macroenabled.12";

            /// <summary>
            /// Declaração de ppsx.
            /// </summary>
            public const string PPSX = "application/vnd.openxmlformats-officedocument.presentationml.slideshow";

            /// <summary>
            /// Declaração de ppt.
            /// </summary>
            public const string PPT = "application/vnd.ms-powerpoint";

            /// <summary>
            /// Declaração de pptm.
            /// </summary>
            public const string PPTM = "application/vnd.ms-powerpoint.presentation.macroenabled.12";

            /// <summary>
            /// Declaração de pptx.
            /// </summary>
            public const string PPTX = "application/vnd.openxmlformats-officedocument.presentationml.presentation";

            /// <summary>
            /// Declaração de prf.
            /// </summary>
            public const string PRF = "application/pics-rules";

            /// <summary>
            /// Declaração de prm.
            /// </summary>
            public const string PRM = "application/octet-stream";

            /// <summary>
            /// Declaração de prx.
            /// </summary>
            public const string PRX = "application/octet-stream";

            /// <summary>
            /// Declaração de ps.
            /// </summary>
            public const string PS = "application/postscript";

            /// <summary>
            /// Declaração de psc1.
            /// </summary>
            public const string PSC1 = "application/powershell";

            /// <summary>
            /// Declaração de psd.
            /// </summary>
            public const string PSD = "application/octet-stream";

            /// <summary>
            /// Declaração de psess.
            /// </summary>
            public const string PSESS = "application/xml";

            /// <summary>
            /// Declaração de psm.
            /// </summary>
            public const string PSM = "application/octet-stream";

            /// <summary>
            /// Declaração de psp.
            /// </summary>
            public const string PSP = "application/octet-stream";

            /// <summary>
            /// Declaração de pub.
            /// </summary>
            public const string PUB = "application/x-mspublisher";

            /// <summary>
            /// Declaração de pwz.
            /// </summary>
            public const string PWZ = "application/vnd.ms-powerpoint";

            /// <summary>
            /// Declaração de qtl.
            /// </summary>
            public const string QTL = "application/x-quicktimeplayer";

            /// <summary>
            /// Declaração de qxd.
            /// </summary>
            public const string QXD = "application/octet-stream";

            /// <summary>
            /// Declaração de rar.
            /// </summary>
            public const string RAR = "application/octet-stream";

            /// <summary>
            /// Declaração de rat.
            /// </summary>
            public const string RAT = "application/rat-file";

            /// <summary>
            /// Declaração de rdlc.
            /// </summary>
            public const string RDLC = "application/xml";

            /// <summary>
            /// Declaração de resx.
            /// </summary>
            public const string RESX = "application/xml";

            /// <summary>
            /// Declaração de rm.
            /// </summary>
            public const string RM = "application/vnd.rn-realmedia";

            /// <summary>
            /// Declaração de rmp.
            /// </summary>
            public const string RMP = "application/vnd.rn-rn_music_package";

            /// <summary>
            /// Declaração de roff.
            /// </summary>
            public const string ROFF = "application/x-troff";

            /// <summary>
            /// Declaração de rtf.
            /// </summary>
            public const string RTF = "application/rtf";

            /// <summary>
            /// Declaração de ruleset.
            /// </summary>
            public const string RULESET = "application/xml";

            /// <summary>
            /// Declaração de safariextz.
            /// </summary>
            public const string SAFARIEXTZ = "application/x-safari-safariextz";

            /// <summary>
            /// Declaração de scd.
            /// </summary>
            public const string SCD = "application/x-msschedule";

            /// <summary>
            /// Declaração de sdp.
            /// </summary>
            public const string SDP = "application/sdp";

            /// <summary>
            /// Declaração de sea.
            /// </summary>
            public const string SEA = "application/octet-stream";

            /// <summary>
            /// Declaração de searchConnector-ms.
            /// </summary>
            public const string SEARCHCONNECTOR_MS = "application/windows-search-connector+xml";

            /// <summary>
            /// Declaração de setpay.
            /// </summary>
            public const string SETPAY = "application/set-payment-initiation";

            /// <summary>
            /// Declaração de setreg.
            /// </summary>
            public const string SETREG = "application/set-registration-initiation";

            /// <summary>
            /// Declaração de settings.
            /// </summary>
            public const string SETTINGS = "application/xml";

            /// <summary>
            /// Declaração de sgimb.
            /// </summary>
            public const string SGIMB = "application/x-sgimb";

            /// <summary>
            /// Declaração de sh.
            /// </summary>
            public const string SH = "application/x-sh";

            /// <summary>
            /// Declaração de shar.
            /// </summary>
            public const string SHAR = "application/x-shar";

            /// <summary>
            /// Declaração de sit.
            /// </summary>
            public const string SIT = "application/x-stuffit";

            /// <summary>
            /// Declaração de sitemap.
            /// </summary>
            public const string SITEMAP = "application/xml";

            /// <summary>
            /// Declaração de skin.
            /// </summary>
            public const string SKIN = "application/xml";

            /// <summary>
            /// Declaração de sldm.
            /// </summary>
            public const string SLDM = "application/vnd.ms-powerpoint.slide.macroenabled.12";

            /// <summary>
            /// Declaração de sldx.
            /// </summary>
            public const string SLDX = "application/vnd.openxmlformats-officedocument.presentationml.slide";

            /// <summary>
            /// Declaração de slk.
            /// </summary>
            public const string SLK = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de slupkg-ms.
            /// </summary>
            public const string SLUPKG_MS = "application/x-ms-license";

            /// <summary>
            /// Declaração de smi.
            /// </summary>
            public const string SMI = "application/octet-stream";

            /// <summary>
            /// Declaração de snippet.
            /// </summary>
            public const string SNIPPET = "application/xml";

            /// <summary>
            /// Declaração de snp.
            /// </summary>
            public const string SNP = "application/octet-stream";

            /// <summary>
            /// Declaração de spc.
            /// </summary>
            public const string SPC = "application/x-pkcs7-certificates";

            /// <summary>
            /// Declaração de spl.
            /// </summary>
            public const string SPL = "application/futuresplash";

            /// <summary>
            /// Declaração de src.
            /// </summary>
            public const string SRC = "application/x-wais-source";

            /// <summary>
            /// Declaração de ssm.
            /// </summary>
            public const string SSM = "application/streamingmedia";

            /// <summary>
            /// Declaração de sst.
            /// </summary>
            public const string SST = "application/vnd.ms-pki.certstore";

            /// <summary>
            /// Declaração de stl.
            /// </summary>
            public const string STL = "application/vnd.ms-pki.stl";

            /// <summary>
            /// Declaração de sv4cpio.
            /// </summary>
            public const string SV4CPIO = "application/x-sv4cpio";

            /// <summary>
            /// Declaração de sv4crc.
            /// </summary>
            public const string SV4CRC = "application/x-sv4crc";

            /// <summary>
            /// Declaração de svc.
            /// </summary>
            public const string SVC = "application/xml";

            /// <summary>
            /// Declaração de swf.
            /// </summary>
            public const string SWF = "application/x-shockwave-flash";

            /// <summary>
            /// Declaração de t.
            /// </summary>
            public const string T = "application/x-troff";

            /// <summary>
            /// Declaração de tar.
            /// </summary>
            public const string TAR = "application/x-tar";

            /// <summary>
            /// Declaração de tcl.
            /// </summary>
            public const string TCL = "application/x-tcl";

            /// <summary>
            /// Declaração de testrunconfig.
            /// </summary>
            public const string TESTRUNCONFIG = "application/xml";

            /// <summary>
            /// Declaração de testsettings.
            /// </summary>
            public const string TESTSETTINGS = "application/xml";

            /// <summary>
            /// Declaração de tex.
            /// </summary>
            public const string TEX = "application/x-tex";

            /// <summary>
            /// Declaração de texi.
            /// </summary>
            public const string TEXI = "application/x-texinfo";

            /// <summary>
            /// Declaração de texinfo.
            /// </summary>
            public const string TEXINFO = "application/x-texinfo";

            /// <summary>
            /// Declaração de tgz.
            /// </summary>
            public const string TGZ = "application/x-compressed";

            /// <summary>
            /// Declaração de thmx.
            /// </summary>
            public const string THMX = "application/vnd.ms-officetheme";

            /// <summary>
            /// Declaração de thn.
            /// </summary>
            public const string THN = "application/octet-stream";

            /// <summary>
            /// Declaração de toc.
            /// </summary>
            public const string TOC = "application/octet-stream";

            /// <summary>
            /// Declaração de tr.
            /// </summary>
            public const string TR = "application/x-troff";

            /// <summary>
            /// Declaração de trm.
            /// </summary>
            public const string TRM = "application/x-msterminal";

            /// <summary>
            /// Declaração de trx.
            /// </summary>
            public const string TRX = "application/xml";

            /// <summary>
            /// Declaração de ttf.
            /// </summary>
            public const string TTF = "application/octet-stream";

            /// <summary>
            /// Declaração de u32.
            /// </summary>
            public const string U32 = "application/octet-stream";

            /// <summary>
            /// Declaração de ustar.
            /// </summary>
            public const string USTAR = "application/x-ustar";

            /// <summary>
            /// Declaração de vdx.
            /// </summary>
            public const string VDX = "application/vnd.ms-visio.viewer";

            /// <summary>
            /// Declaração de vscontent.
            /// </summary>
            public const string VSCONTENT = "application/xml";

            /// <summary>
            /// Declaração de vsd.
            /// </summary>
            public const string VSD = "application/vnd.visio";

            /// <summary>
            /// Declaração de vsi.
            /// </summary>
            public const string VSI = "application/ms-vsi";

            /// <summary>
            /// Declaração de vsix.
            /// </summary>
            public const string VSIX = "application/vsix";

            /// <summary>
            /// Declaração de vsmdi.
            /// </summary>
            public const string VSMDI = "application/xml";

            /// <summary>
            /// Declaração de vss.
            /// </summary>
            public const string VSS = "application/vnd.visio";

            /// <summary>
            /// Declaração de vst.
            /// </summary>
            public const string VST = "application/vnd.visio";

            /// <summary>
            /// Declaração de vsto.
            /// </summary>
            public const string VSTO = "application/x-ms-vsto";

            /// <summary>
            /// Declaração de vsw.
            /// </summary>
            public const string VSW = "application/vnd.visio";

            /// <summary>
            /// Declaração de vsx.
            /// </summary>
            public const string VSX = "application/vnd.visio";

            /// <summary>
            /// Declaração de vtx.
            /// </summary>
            public const string VTX = "application/vnd.visio";

            /// <summary>
            /// Declaração de wbk.
            /// </summary>
            public const string WBK = "application/msword";

            /// <summary>
            /// Declaração de wcm.
            /// </summary>
            public const string WCM = "application/vnd.ms-works";

            /// <summary>
            /// Declaração de wdb.
            /// </summary>
            public const string WDB = "application/vnd.ms-works";

            /// <summary>
            /// Declaração de webarchive.
            /// </summary>
            public const string WEBARCHIVE = "application/x-safari-webarchive";

            /// <summary>
            /// Declaração de webtest.
            /// </summary>
            public const string WEBTEST = "application/xml";

            /// <summary>
            /// Declaração de wiq.
            /// </summary>
            public const string WIQ = "application/xml";

            /// <summary>
            /// Declaração de wiz.
            /// </summary>
            public const string WIZ = "application/msword";

            /// <summary>
            /// Declaração de wks.
            /// </summary>
            public const string WKS = "application/vnd.ms-works";

            /// <summary>
            /// Declaração de WLMP.
            /// </summary>
            public const string WLMP = "application/wlmoviemaker";

            /// <summary>
            /// Declaração de wlpginstall.
            /// </summary>
            public const string WLPGINSTALL = "application/x-wlpg-detect";

            /// <summary>
            /// Declaração de wlpginstall3.
            /// </summary>
            public const string WLPGINSTALL3 = "application/x-wlpg3-detect";

            /// <summary>
            /// Declaração de wmd.
            /// </summary>
            public const string WMD = "application/x-ms-wmd";

            /// <summary>
            /// Declaração de wmf.
            /// </summary>
            public const string WMF = "application/x-msmetafile";

            /// <summary>
            /// Declaração de wmlc.
            /// </summary>
            public const string WMLC = "application/vnd.wap.wmlc";

            /// <summary>
            /// Declaração de wmlsc.
            /// </summary>
            public const string WMLSC = "application/vnd.wap.wmlscriptc";

            /// <summary>
            /// Declaração de wmz.
            /// </summary>
            public const string WMZ = "application/x-ms-wmz";

            /// <summary>
            /// Declaração de wpl.
            /// </summary>
            public const string WPL = "application/vnd.ms-wpl";

            /// <summary>
            /// Declaração de wps.
            /// </summary>
            public const string WPS = "application/vnd.ms-works";

            /// <summary>
            /// Declaração de wri.
            /// </summary>
            public const string WRI = "application/x-mswrite";

            /// <summary>
            /// Declaração de x.
            /// </summary>
            public const string X = "application/directx";

            /// <summary>
            /// Declaração de xaml.
            /// </summary>
            public const string XAML = "application/xaml+xml";

            /// <summary>
            /// Declaração de xap.
            /// </summary>
            public const string XAP = "application/x-silverlight-app";

            /// <summary>
            /// Declaração de xbap.
            /// </summary>
            public const string XBAP = "application/x-ms-xbap";

            /// <summary>
            /// Declaração de xht.
            /// </summary>
            public const string XHT = "application/xhtml+xml";

            /// <summary>
            /// Declaração de xhtml.
            /// </summary>
            public const string XHTML = "application/xhtml+xml";

            /// <summary>
            /// Declaração de xla.
            /// </summary>
            public const string XLA = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xlam.
            /// </summary>
            public const string XLAM = "application/vnd.ms-excel.addin.macroenabled.12";

            /// <summary>
            /// Declaração de xlc.
            /// </summary>
            public const string XLC = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xld.
            /// </summary>
            public const string XLD = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xlk.
            /// </summary>
            public const string XLK = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xll.
            /// </summary>
            public const string XLL = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xlm.
            /// </summary>
            public const string XLM = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xls.
            /// </summary>
            public const string XLS = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xlsb.
            /// </summary>
            public const string XLSB = "application/vnd.ms-excel.sheet.binary.macroenabled.12";

            /// <summary>
            /// Declaração de xlsm.
            /// </summary>
            public const string XLSM = "application/vnd.ms-excel.sheet.macroenabled.12";

            /// <summary>
            /// Declaração de xlsx.
            /// </summary>
            public const string XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            /// <summary>
            /// Declaração de xlt.
            /// </summary>
            public const string XLT = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xltm.
            /// </summary>
            public const string XLTM = "application/vnd.ms-excel.template.macroenabled.12";

            /// <summary>
            /// Declaração de xltx.
            /// </summary>
            public const string XLTX = "application/vnd.openxmlformats-officedocument.spreadsheetml.template";

            /// <summary>
            /// Declaração de xlw.
            /// </summary>
            public const string XLW = "application/vnd.ms-excel";

            /// <summary>
            /// Declaração de xmta.
            /// </summary>
            public const string XMTA = "application/xml";

            /// <summary>
            /// Declaração de xps.
            /// </summary>
            public const string XPS = "application/vnd.ms-xpsdocument";

            /// <summary>
            /// Declaração de xsc.
            /// </summary>
            public const string XSC = "application/xml";

            /// <summary>
            /// Declaração de xsn.
            /// </summary>
            public const string XSN = "application/octet-stream";

            /// <summary>
            /// Declaração de xss.
            /// </summary>
            public const string XSS = "application/xml";

            /// <summary>
            /// Declaração de xtp.
            /// </summary>
            public const string XTP = "application/octet-stream";

            /// <summary>
            /// Declaração de z.
            /// </summary>
            public const string Z = "application/x-compress";

            /// <summary>
            /// Declaração de zip.
            /// </summary>
            public const string ZIP = "application/x-zip-compressed";


        }

        /// <summary>
        /// Mimetypes do tipo 'Audio'.
        /// </summary>
        public static class Audio
        {
            /// <summary>
            /// Declaração de aa.
            /// </summary>
            public const string AA = "audio/audible";

            /// <summary>
            /// Declaração de AAC.
            /// </summary>
            public const string AAC = "audio/aac";

            /// <summary>
            /// Declaração de aax.
            /// </summary>
            public const string AAX = "audio/vnd.audible.aax";

            /// <summary>
            /// Declaração de ac3.
            /// </summary>
            public const string AC3 = "audio/ac3";

            /// <summary>
            /// Declaração de ADT.
            /// </summary>
            public const string ADT = "audio/vnd.dlna.adts";

            /// <summary>
            /// Declaração de ADTS.
            /// </summary>
            public const string ADTS = "audio/aac";

            /// <summary>
            /// Declaração de aif.
            /// </summary>
            public const string AIF = "audio/x-aiff";

            /// <summary>
            /// Declaração de aifc.
            /// </summary>
            public const string AIFC = "audio/aiff";

            /// <summary>
            /// Declaração de aiff.
            /// </summary>
            public const string AIFF = "audio/aiff";

            /// <summary>
            /// Declaração de au.
            /// </summary>
            public const string AU = "audio/basic";

            /// <summary>
            /// Declaração de caf.
            /// </summary>
            public const string CAF = "audio/x-caf";

            /// <summary>
            /// Declaração de cdda.
            /// </summary>
            public const string CDDA = "audio/aiff";

            /// <summary>
            /// Declaração de gsm.
            /// </summary>
            public const string GSM = "audio/x-gsm";

            /// <summary>
            /// Declaração de m3u.
            /// </summary>
            public const string M3U = "audio/x-mpegurl";

            /// <summary>
            /// Declaração de m3u8.
            /// </summary>
            public const string M3U8 = "audio/x-mpegurl";

            /// <summary>
            /// Declaração de m4a.
            /// </summary>
            public const string M4A = "audio/m4a";

            /// <summary>
            /// Declaração de m4b.
            /// </summary>
            public const string M4B = "audio/m4b";

            /// <summary>
            /// Declaração de m4p.
            /// </summary>
            public const string M4P = "audio/m4p";

            /// <summary>
            /// Declaração de m4r.
            /// </summary>
            public const string M4R = "audio/x-m4r";

            /// <summary>
            /// Declaração de mid.
            /// </summary>
            public const string MID = "audio/mid";

            /// <summary>
            /// Declaração de midi.
            /// </summary>
            public const string MIDI = "audio/mid";

            /// <summary>
            /// Declaração de mp3.
            /// </summary>
            public const string MP3 = "audio/mpeg";

            /// <summary>
            /// Declaração de pls.
            /// </summary>
            public const string PLS = "audio/scpls";

            /// <summary>
            /// Declaração de ra.
            /// </summary>
            public const string RA = "audio/x-pn-realaudio";

            /// <summary>
            /// Declaração de ram.
            /// </summary>
            public const string RAM = "audio/x-pn-realaudio";

            /// <summary>
            /// Declaração de rmi.
            /// </summary>
            public const string RMI = "audio/mid";

            /// <summary>
            /// Declaração de rpm.
            /// </summary>
            public const string RPM = "audio/x-pn-realaudio-plugin";

            /// <summary>
            /// Declaração de sd2.
            /// </summary>
            public const string SD2 = "audio/x-sd2";

            /// <summary>
            /// Declaração de smd.
            /// </summary>
            public const string SMD = "audio/x-smd";

            /// <summary>
            /// Declaração de smx.
            /// </summary>
            public const string SMX = "audio/x-smd";

            /// <summary>
            /// Declaração de smz.
            /// </summary>
            public const string SMZ = "audio/x-smd";

            /// <summary>
            /// Declaração de snd.
            /// </summary>
            public const string SND = "audio/basic";

            /// <summary>
            /// Declaração de wav.
            /// </summary>
            public const string WAV = "audio/wav";

            /// <summary>
            /// Declaração de wave.
            /// </summary>
            public const string WAVE = "audio/wav";

            /// <summary>
            /// Declaração de wax.
            /// </summary>
            public const string WAX = "audio/x-ms-wax";

            /// <summary>
            /// Declaração de wma.
            /// </summary>
            public const string WMA = "audio/x-ms-wma";
        }

        /// <summary>
        /// Mimetypes do tipo 'Image'.
        /// </summary>
        public static class Image
        {
            /// <summary>
            /// Declaração de art.
            /// </summary>
            public const string ART = "image/x-jg";

            /// <summary>
            /// Declaração de bmp.
            /// </summary>
            public const string BMP = "image/bmp";

            /// <summary>
            /// Declaração de cmx.
            /// </summary>
            public const string CMX = "image/x-cmx";

            /// <summary>
            /// Declaração de cod.
            /// </summary>
            public const string COD = "image/cis-cod";

            /// <summary>
            /// Declaração de dib.
            /// </summary>
            public const string DIB = "image/bmp";

            /// <summary>
            /// Declaração de gif.
            /// </summary>
            public const string GIF = "image/gif";

            /// <summary>
            /// Declaração de ico.
            /// </summary>
            public const string ICO = "image/x-icon";

            /// <summary>
            /// Declaração de ief.
            /// </summary>
            public const string IEF = "image/ief";

            /// <summary>
            /// Declaração de jfif.
            /// </summary>
            public const string JFIF = "image/pjpeg";

            /// <summary>
            /// Declaração de jpe.
            /// </summary>
            public const string JPE = "image/jpeg";

            /// <summary>
            /// Declaração de jpeg.
            /// </summary>
            public const string JPEG = "image/jpeg";

            /// <summary>
            /// Declaração de jpg.
            /// </summary>
            public const string JPG = "image/jpeg";

            /// <summary>
            /// Declaração de mac.
            /// </summary>
            public const string MAC = "image/x-macpaint";

            /// <summary>
            /// Declaração de pbm.
            /// </summary>
            public const string PBM = "image/x-portable-bitmap";

            /// <summary>
            /// Declaração de pct.
            /// </summary>
            public const string PCT = "image/pict";

            /// <summary>
            /// Declaração de pgm.
            /// </summary>
            public const string PGM = "image/x-portable-graymap";

            /// <summary>
            /// Declaração de pic.
            /// </summary>
            public const string PIC = "image/pict";

            /// <summary>
            /// Declaração de pict.
            /// </summary>
            public const string PICT = "image/pict";

            /// <summary>
            /// Declaração de png.
            /// </summary>
            public const string PNG = "image/png";

            /// <summary>
            /// Declaração de pnm.
            /// </summary>
            public const string PNM = "image/x-portable-anymap";

            /// <summary>
            /// Declaração de pnt.
            /// </summary>
            public const string PNT = "image/x-macpaint";

            /// <summary>
            /// Declaração de pntg.
            /// </summary>
            public const string PNTG = "image/x-macpaint";

            /// <summary>
            /// Declaração de pnz.
            /// </summary>
            public const string PNZ = "image/png";

            /// <summary>
            /// Declaração de ppm.
            /// </summary>
            public const string PPM = "image/x-portable-pixmap";

            /// <summary>
            /// Declaração de qti.
            /// </summary>
            public const string QTI = "image/x-quicktime";

            /// <summary>
            /// Declaração de qtif.
            /// </summary>
            public const string QTIF = "image/x-quicktime";

            /// <summary>
            /// Declaração de ras.
            /// </summary>
            public const string RAS = "image/x-cmu-raster";

            /// <summary>
            /// Declaração de rf.
            /// </summary>
            public const string RF = "image/vnd.rn-realflash";

            /// <summary>
            /// Declaração de rgb.
            /// </summary>
            public const string RGB = "image/x-rgb";

            /// <summary>
            /// Declaração de tif.
            /// </summary>
            public const string TIF = "image/tiff";

            /// <summary>
            /// Declaração de tiff.
            /// </summary>
            public const string TIFF = "image/tiff";

            /// <summary>
            /// Declaração de wbmp.
            /// </summary>
            public const string WBMP = "image/vnd.wap.wbmp";

            /// <summary>
            /// Declaração de wdp.
            /// </summary>
            public const string WDP = "image/vnd.ms-photo";

            /// <summary>
            /// Declaração de xbm.
            /// </summary>
            public const string XBM = "image/x-xbitmap";

            /// <summary>
            /// Declaração de xpm.
            /// </summary>
            public const string XPM = "image/x-xpixmap";

            /// <summary>
            /// Declaração de xwd.
            /// </summary>
            public const string XWD = "image/x-xwindowdump";


        }

        /// <summary>
        /// Mimetypes do tipo 'texto'.
        /// </summary>
        public static class Text
        {
            /// <summary>
            /// Declaração de H323.
            /// </summary>
            public const string H323 = "text/h323";

            /// <summary>
            /// Declaração de AddIn.
            /// </summary>
            public const string ADDIN = "text/xml";

            /// <summary>
            /// Declaração de asm.
            /// </summary>
            public const string ASM = "text/plain";

            /// <summary>
            /// Declaração de bas.
            /// </summary>
            public const string BAS = "text/plain";

            /// <summary>
            /// Declaração de c.
            /// </summary>
            public const string C = "text/plain";

            /// <summary>
            /// Declaração de cc.
            /// </summary>
            public const string CC = "text/plain";

            /// <summary>
            /// Declaração de cd.
            /// </summary>
            public const string CD = "text/plain";

            /// <summary>
            /// Declaração de cnf.
            /// </summary>
            public const string CNF = "text/plain";

            /// <summary>
            /// Declaração de contact.
            /// </summary>
            public const string CONTACT = "text/x-ms-contact";

            /// <summary>
            /// Declaração de cpp.
            /// </summary>
            public const string CPP = "text/plain";

            /// <summary>
            /// Declaração de cs.
            /// </summary>
            public const string CS = "text/plain";

            /// <summary>
            /// Declaração de csdproj.
            /// </summary>
            public const string CSDPROJ = "text/plain";

            /// <summary>
            /// Declaração de csproj.
            /// </summary>
            public const string CSPROJ = "text/plain";

            /// <summary>
            /// Declaração de css.
            /// </summary>
            public const string CSS = "text/css";

            /// <summary>
            /// Declaração de csv.
            /// </summary>
            public const string CSV = "text/csv";

            /// <summary>
            /// Declaração de cxx.
            /// </summary>
            public const string CXX = "text/plain";

            /// <summary>
            /// Declaração de dbproj.
            /// </summary>
            public const string DBPROJ = "text/plain";

            /// <summary>
            /// Declaração de def.
            /// </summary>
            public const string DEF = "text/plain";

            /// <summary>
            /// Declaração de disco.
            /// </summary>
            public const string DISCO = "text/xml";

            /// <summary>
            /// Declaração de dllconfig.
            /// </summary>
            public const string DLLCONFIG = "text/xml";

            /// <summary>
            /// Declaração de dlm.
            /// </summary>
            public const string DLM = "text/dlm";

            /// <summary>
            /// Declaração de dsw.
            /// </summary>
            public const string DSW = "text/plain";

            /// <summary>
            /// Declaração de dtd.
            /// </summary>
            public const string DTD = "text/xml";

            /// <summary>
            /// Declaração de dtsConfig.
            /// </summary>
            public const string DTSCONFIG = "text/xml";

            /// <summary>
            /// Declaração de etx.
            /// </summary>
            public const string ETX = "text/x-setext";

            /// <summary>
            /// Declaração de execonfig.
            /// </summary>
            public const string EXECONFIG = "text/xml";

            /// <summary>
            /// Declaração de group.
            /// </summary>
            public const string GROUP = "text/x-ms-group";

            /// <summary>
            /// Declaração de h.
            /// </summary>
            public const string H = "text/plain";

            /// <summary>
            /// Declaração de hdml.
            /// </summary>
            public const string HDML = "text/x-hdml";

            /// <summary>
            /// Declaração de hpp.
            /// </summary>
            public const string HPP = "text/plain";

            /// <summary>
            /// Declaração de htc.
            /// </summary>
            public const string HTC = "text/x-component";

            /// <summary>
            /// Declaração de htm.
            /// </summary>
            public const string HTM = "text/html";

            /// <summary>
            /// Declaração de html.
            /// </summary>
            public const string HTML = "text/html";

            /// <summary>
            /// Declaração de htt.
            /// </summary>
            public const string HTT = "text/webviewhtml";

            /// <summary>
            /// Declaração de hxt.
            /// </summary>
            public const string HXT = "text/html";

            /// <summary>
            /// Declaração de hxx.
            /// </summary>
            public const string HXX = "text/plain";

            /// <summary>
            /// Declaração de i.
            /// </summary>
            public const string I = "text/plain";

            /// <summary>
            /// Declaração de idl.
            /// </summary>
            public const string IDL = "text/plain";

            /// <summary>
            /// Declaração de inc.
            /// </summary>
            public const string INC = "text/plain";

            /// <summary>
            /// Declaração de inl.
            /// </summary>
            public const string INL = "text/plain";

            /// <summary>
            /// Declaração de ipproj.
            /// </summary>
            public const string IPPROJ = "text/plain";

            /// <summary>
            /// Declaração de iqy.
            /// </summary>
            public const string IQY = "text/x-ms-iqy";

            /// <summary>
            /// Declaração de jsx.
            /// </summary>
            public const string JSX = "text/jscript";

            /// <summary>
            /// Declaração de jsxbin.
            /// </summary>
            public const string JSXBIN = "text/plain";

            /// <summary>
            /// Declaração de lst.
            /// </summary>
            public const string LST = "text/plain";

            /// <summary>
            /// Declaração de mak.
            /// </summary>
            public const string MAK = "text/plain";

            /// <summary>
            /// Declaração de map.
            /// </summary>
            public const string MAP = "text/plain";

            /// <summary>
            /// Declaração de mk.
            /// </summary>
            public const string MK = "text/plain";

            /// <summary>
            /// Declaração de mno.
            /// </summary>
            public const string MNO = "text/xml";

            /// <summary>
            /// Declaração de odc.
            /// </summary>
            public const string ODC = "text/x-ms-odc";

            /// <summary>
            /// Declaração de odh.
            /// </summary>
            public const string ODH = "text/plain";

            /// <summary>
            /// Declaração de odl.
            /// </summary>
            public const string ODL = "text/plain";

            /// <summary>
            /// Declaração de pkgdef.
            /// </summary>
            public const string PKGDEF = "text/plain";

            /// <summary>
            /// Declaração de pkgundef.
            /// </summary>
            public const string PKGUNDEF = "text/plain";

            /// <summary>
            /// Declaração de qht.
            /// </summary>
            public const string QHT = "text/x-html-insertion";

            /// <summary>
            /// Declaração de qhtm.
            /// </summary>
            public const string QHTM = "text/x-html-insertion";

            /// <summary>
            /// Declaração de rc.
            /// </summary>
            public const string RC = "text/plain";

            /// <summary>
            /// Declaração de rc2.
            /// </summary>
            public const string RC2 = "text/plain";

            /// <summary>
            /// Declaração de rct.
            /// </summary>
            public const string RCT = "text/plain";

            /// <summary>
            /// Declaração de rgs.
            /// </summary>
            public const string RGS = "text/plain";

            /// <summary>
            /// Declaração de rqy.
            /// </summary>
            public const string RQY = "text/x-ms-rqy";

            /// <summary>
            /// Declaração de rtx.
            /// </summary>
            public const string RTX = "text/richtext";

            /// <summary>
            /// Declaração de s.
            /// </summary>
            public const string S = "text/plain";

            /// <summary>
            /// Declaração de sct.
            /// </summary>
            public const string SCT = "text/scriptlet";

            /// <summary>
            /// Declaração de sgml.
            /// </summary>
            public const string SGML = "text/sgml";

            /// <summary>
            /// Declaração de shtml.
            /// </summary>
            public const string SHTML = "text/html";

            /// <summary>
            /// Declaração de sln.
            /// </summary>
            public const string SLN = "text/plain";

            /// <summary>
            /// Declaração de sol.
            /// </summary>
            public const string SOL = "text/plain";

            /// <summary>
            /// Declaração de sor.
            /// </summary>
            public const string SOR = "text/plain";

            /// <summary>
            /// Declaração de srf.
            /// </summary>
            public const string SRF = "text/plain";

            /// <summary>
            /// Declaração de SSISDeploymentManifest.
            /// </summary>
            public const string SSISDEPLOYMENTMANIFEST = "text/xml";

            /// <summary>
            /// Declaração de tlh.
            /// </summary>
            public const string TLH = "text/plain";

            /// <summary>
            /// Declaração de tli.
            /// </summary>
            public const string TLI = "text/plain";

            /// <summary>
            /// Declaração de tsv.
            /// </summary>
            public const string TSV = "text/tab-separated-values";

            /// <summary>
            /// Declaração de txt.
            /// </summary>
            public const string TXT = "text/plain";

            /// <summary>
            /// Declaração de uls.
            /// </summary>
            public const string ULS = "text/iuls";

            /// <summary>
            /// Declaração de user.
            /// </summary>
            public const string USER = "text/plain";

            /// <summary>
            /// Declaração de vb.
            /// </summary>
            public const string VB = "text/plain";

            /// <summary>
            /// Declaração de vbdproj.
            /// </summary>
            public const string VBDPROJ = "text/plain";

            /// <summary>
            /// Declaração de vbproj.
            /// </summary>
            public const string VBPROJ = "text/plain";

            /// <summary>
            /// Declaração de vbs.
            /// </summary>
            public const string VBS = "text/vbscript";

            /// <summary>
            /// Declaração de vcf.
            /// </summary>
            public const string VCF = "text/x-vcard";

            /// <summary>
            /// Declaração de vcs.
            /// </summary>
            public const string VCS = "text/plain";

            /// <summary>
            /// Declaração de vddproj.
            /// </summary>
            public const string VDDPROJ = "text/plain";

            /// <summary>
            /// Declaração de vdp.
            /// </summary>
            public const string VDP = "text/plain";

            /// <summary>
            /// Declaração de vdproj.
            /// </summary>
            public const string VDPROJ = "text/plain";

            /// <summary>
            /// Declaração de vml.
            /// </summary>
            public const string VML = "text/xml";

            /// <summary>
            /// Declaração de vsct.
            /// </summary>
            public const string VSCT = "text/xml";

            /// <summary>
            /// Declaração de vsixlangpack.
            /// </summary>
            public const string VSIXLANGPACK = "text/xml";

            /// <summary>
            /// Declaração de vsixmanifest.
            /// </summary>
            public const string VSIXMANIFEST = "text/xml";

            /// <summary>
            /// Declaração de vspscc.
            /// </summary>
            public const string VSPSCC = "text/plain";

            /// <summary>
            /// Declaração de vsscc.
            /// </summary>
            public const string VSSCC = "text/plain";

            /// <summary>
            /// Declaração de vssettings.
            /// </summary>
            public const string VSSETTINGS = "text/xml";

            /// <summary>
            /// Declaração de vssscc.
            /// </summary>
            public const string VSSSCC = "text/plain";

            /// <summary>
            /// Declaração de vstemplate.
            /// </summary>
            public const string VSTEMPLATE = "text/xml";

            /// <summary>
            /// Declaração de wml.
            /// </summary>
            public const string WML = "text/vnd.wap.wml";

            /// <summary>
            /// Declaração de wmls.
            /// </summary>
            public const string WMLS = "text/vnd.wap.wmlscript";

            /// <summary>
            /// Declaração de wsc.
            /// </summary>
            public const string WSC = "text/scriptlet";

            /// <summary>
            /// Declaração de wsdl.
            /// </summary>
            public const string WSDL = "text/xml";

            /// <summary>
            /// Declaração de xdr.
            /// </summary>
            public const string XDR = "text/plain";

            /// <summary>
            /// Declaração de xml.
            /// </summary>
            public const string XML = "text/xml";

            /// <summary>
            /// Declaração de XOML.
            /// </summary>
            public const string XOML = "text/plain";

            /// <summary>
            /// Declaração de xrm-ms.
            /// </summary>
            public const string XRM_MS = "text/xml";

            /// <summary>
            /// Declaração de xsd.
            /// </summary>
            public const string XSD = "text/xml";

            /// <summary>
            /// Declaração de xsf.
            /// </summary>
            public const string XSF = "text/xml";

            /// <summary>
            /// Declaração de xsl.
            /// </summary>
            public const string XSL = "text/xml";

            /// <summary>
            /// Declaração de xslt.
            /// </summary>
            public const string XSLT = "text/xml";
        }

        /// <summary>
        /// Mimetypes do tipo 'Video'.
        /// </summary>
        public static class Video
        {
            /// <summary>
            /// Declaração de 3g2.
            /// </summary>
            public const string V3G2 = "video/3gpp2";

            /// <summary>
            /// Declaração de 3gp.
            /// </summary>
            public const string V3GP = "video/3gpp";

            /// <summary>
            /// Declaração de 3gp2.
            /// </summary>
            public const string V3GP2 = "video/3gpp2";

            /// <summary>
            /// Declaração de 3gpp.
            /// </summary>
            public const string V3GPP = "video/3gpp";

            /// <summary>
            /// Declaração de asf.
            /// </summary>
            public const string ASF = "video/x-ms-asf";

            /// <summary>
            /// Declaração de asr.
            /// </summary>
            public const string ASR = "video/x-ms-asf";

            /// <summary>
            /// Declaração de asx.
            /// </summary>
            public const string ASX = "video/x-ms-asf";

            /// <summary>
            /// Declaração de avi.
            /// </summary>
            public const string AVI = "video/x-msvideo";

            /// <summary>
            /// Declaração de dif.
            /// </summary>
            public const string DIF = "video/x-dv";

            /// <summary>
            /// Declaração de dv.
            /// </summary>
            public const string DV = "video/x-dv";

            /// <summary>
            /// Declaração de flv.
            /// </summary>
            public const string FLV = "video/x-flv";

            /// <summary>
            /// Declaração de IVF.
            /// </summary>
            public const string IVF = "video/x-ivf";

            /// <summary>
            /// Declaração de lsf.
            /// </summary>
            public const string LSF = "video/x-la-asf";

            /// <summary>
            /// Declaração de lsx.
            /// </summary>
            public const string LSX = "video/x-la-asf";

            /// <summary>
            /// Declaração de m1v.
            /// </summary>
            public const string M1V = "video/mpeg";

            /// <summary>
            /// Declaração de m2t.
            /// </summary>
            public const string M2T = "video/vnd.dlna.mpeg-tts";

            /// <summary>
            /// Declaração de m2ts.
            /// </summary>
            public const string M2TS = "video/vnd.dlna.mpeg-tts";

            /// <summary>
            /// Declaração de m2v.
            /// </summary>
            public const string M2V = "video/mpeg";

            /// <summary>
            /// Declaração de m4v.
            /// </summary>
            public const string M4V = "video/x-m4v";

            /// <summary>
            /// Declaração de mod.
            /// </summary>
            public const string MOD = "video/mpeg";

            /// <summary>
            /// Declaração de mov.
            /// </summary>
            public const string MOV = "video/quicktime";

            /// <summary>
            /// Declaração de movie.
            /// </summary>
            public const string MOVIE = "video/x-sgi-movie";

            /// <summary>
            /// Declaração de mp2.
            /// </summary>
            public const string MP2 = "video/mpeg";

            /// <summary>
            /// Declaração de mp2v.
            /// </summary>
            public const string MP2V = "video/mpeg";

            /// <summary>
            /// Declaração de mp4.
            /// </summary>
            public const string MP4 = "video/mp4";

            /// <summary>
            /// Declaração de mp4v.
            /// </summary>
            public const string MP4V = "video/mp4";

            /// <summary>
            /// Declaração de mpa.
            /// </summary>
            public const string MPA = "video/mpeg";

            /// <summary>
            /// Declaração de mpe.
            /// </summary>
            public const string MPE = "video/mpeg";

            /// <summary>
            /// Declaração de mpeg.
            /// </summary>
            public const string MPEG = "video/mpeg";

            /// <summary>
            /// Declaração de mpg.
            /// </summary>
            public const string MPG = "video/mpeg";

            /// <summary>
            /// Declaração de mpv2.
            /// </summary>
            public const string MPV2 = "video/mpeg";

            /// <summary>
            /// Declaração de mqv.
            /// </summary>
            public const string MQV = "video/quicktime";

            /// <summary>
            /// Declaração de mts.
            /// </summary>
            public const string MTS = "video/vnd.dlna.mpeg-tts";

            /// <summary>
            /// Declaração de nsc.
            /// </summary>
            public const string NSC = "video/x-ms-asf";

            /// <summary>
            /// Declaração de qt.
            /// </summary>
            public const string QT = "video/quicktime";

            /// <summary>
            /// Declaração de ts.
            /// </summary>
            public const string TS = "video/vnd.dlna.mpeg-tts";

            /// <summary>
            /// Declaração de tts.
            /// </summary>
            public const string TTS = "video/vnd.dlna.mpeg-tts";

            /// <summary>
            /// Declaração de vbk.
            /// </summary>
            public const string VBK = "video/mpeg";

            /// <summary>
            /// Declaração de wm.
            /// </summary>
            public const string WM = "video/x-ms-wm";

            /// <summary>
            /// Declaração de wmp.
            /// </summary>
            public const string WMP = "video/x-ms-wmp";

            /// <summary>
            /// Declaração de wmv.
            /// </summary>
            public const string WMV = "video/x-ms-wmv";

            /// <summary>
            /// Declaração de wmx.
            /// </summary>
            public const string WMX = "video/x-ms-wmx";

            /// <summary>
            /// Declaração de wvx.
            /// </summary>
            public const string WVX = "video/x-ms-wvx";
        }
    }
}
