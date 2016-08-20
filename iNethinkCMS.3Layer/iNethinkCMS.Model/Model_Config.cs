/*******************************************************************************
 * iNethinkCMS - 网站内容管理系统
 * Copyright (C) 2012-2013 inethink.com
 * 
 * @author jackyang <69991000@qq.com>
 * @website http://cms.inethink.com
 * @version 1.3.6.0 (2013-08-14)
 * 
 * This is licensed under the GNU LGPL, version 3.0 or later.
 * For details, see: http://www.gnu.org/licenses/gpl-3.0.html
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace iNethinkCMS.Model
{
    [Serializable]
    public partial class Model_Config
    {
        public Model_Config()
        {
        }

        //系统设置
        private string _webname;
        private string _installdir;
        private int _urlmode;
        private string _templatecache;
        private string _webpagecache;
        private string _cachekey;
        private string _cachetime;
        private string _templatedir;

        //高级设置
        private string _indextemplatename;
        private string _debugmode;

        //伪静态
        private string _rewriteextname;
        private string _rewritechannelprefix;
        private string _rewritespecialprefix;
        private string _rewritecontentprefix;
        private string _rewriteguestbookprefix;

        private string _remoteimgdown;
        private string _upfiletype;
        private string _upfilenaxsize;

        //SEO
        private string _autodescription;
        private string _seotitle;
        private string _indexkeywords;
        private string _indexdescription;

        //个性设置
        private string _pagelistnum;
        private string _displaytitlerule;
        private string _imageseconds;

        #region Model-系统设置
        /// <summary>
        /// 
        /// </summary>
        public string WebName
        {
            set
            {
                _webname = value;
            }
            get
            {
                return _webname;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InstallDir
        {
            set
            {
                _installdir = value;
            }
            get
            {
                return _installdir;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UrlMode
        {
            set
            {
                _urlmode = value;
            }
            get
            {
                return _urlmode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateCache
        {
            set
            {
                _templatecache = value;
            }
            get
            {
                return _templatecache;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WebPageCache
        {
            set
            {
                _webpagecache = value;
            }
            get
            {
                return _webpagecache;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CacheKey
        {
            set
            {
                _cachekey = value;
            }
            get
            {
                return _cachekey;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CacheTime
        {
            set
            {
                _cachetime = value;
            }
            get
            {
                return _cachetime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateDir
        {
            set
            {
                _templatedir = value;
            }
            get
            {
                return _templatedir;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IndexTemplateName
        {
            set
            {
                _indextemplatename = value;
            }
            get
            {
                return _indextemplatename;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DebugMode
        {
            set
            {
                _debugmode = value;
            }
            get
            {
                return _debugmode;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string RewriteExtName
        {
            set
            {
                _rewriteextname = value;
            }
            get
            {
                return _rewriteextname;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RewriteChannelPrefix
        {
            set
            {
                _rewritechannelprefix = value;
            }
            get
            {
                return _rewritechannelprefix;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RewriteSpecialPrefix
        {
            set
            {
                _rewritespecialprefix = value;
            }
            get
            {
                return _rewritespecialprefix;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RewriteContentPrefix
        {
            set
            {
                _rewritecontentprefix = value;
            }
            get
            {
                return _rewritecontentprefix;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RewriteGuestbookPrefix
        {
            set
            {
                _rewriteguestbookprefix = value;
            }
            get
            {
                return _rewriteguestbookprefix;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RemoteImgDown
        {
            set
            {
                _remoteimgdown = value;
            }
            get
            {
                return _remoteimgdown;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UpFileType
        {
            set
            {
                _upfiletype = value;
            }
            get
            {
                return _upfiletype;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UpFileMaxSize
        {
            set
            {
                _upfilenaxsize = value;
            }
            get
            {
                return _upfilenaxsize;
            }
        }
        #endregion

        #region Model-SEO
        /// <summary>
        /// 
        /// </summary>
        public string AutoDescription
        {
            set
            {
                _autodescription = value;
            }
            get
            {
                return _autodescription;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SeoTitle
        {
            set
            {
                _seotitle = value;
            }
            get
            {
                return _seotitle;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IndexKeywords
        {
            set
            {
                _indexkeywords = value;
            }
            get
            {
                return _indexkeywords;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IndexDescription
        {
            set
            {
                _indexdescription = value;
            }
            get
            {
                return _indexdescription;
            }
        }
        #endregion

        #region Model-个性设置
        /// <summary>
        /// 
        /// </summary>
        public string PageListNum
        {
            set
            {
                _pagelistnum = value;
            }
            get
            {
                return _pagelistnum;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayTitleRule
        {
            set
            {
                _displaytitlerule = value;
            }
            get
            {
                return _displaytitlerule;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageSeconds
        {
            set
            {
                _imageseconds = value;
            }
            get
            {
                return _imageseconds;
            }
        }
        #endregion

    }
}
