using Microsoft.SqlServer.Dac.CodeAnalysis;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SampleRules.Attributes
{
    internal class LocalizedExportCodeAnalysisRuleAttribute
        : ExportCodeAnalysisRuleAttribute
    {
        private readonly string resourceBaseName;
        private readonly string displayNameResourceId;
        private readonly string descriptionResourceId;

        private ResourceManager resourceManager;
        private string displayName;
        private string descriptionValue;

        /// <summary>  
        /// Creates the attribute, with the specified rule ID, the fully qualified  
        /// name of the resource file that will be used for looking up display name  
        /// and description, and the Ids of those resources inside the resource file.  
        /// </summary>  
        public LocalizedExportCodeAnalysisRuleAttribute(string id, string resourceBaseName, string displayNameResourceId, string descriptionResourceId)
            : base(id, null)
        {
            this.resourceBaseName = resourceBaseName;
            this.displayNameResourceId = displayNameResourceId;
            this.descriptionResourceId = descriptionResourceId;
        }

        /// <summary>  
        /// Rules in a different assembly would need to overwrite this  
        /// </summary>  
        /// <returns></returns>  
        protected virtual Assembly GetAssembly()
        {
            return GetType().Assembly;
        }

        private void EnsureResourceManagerInitialized()
        {
            var resourceAssembly = GetAssembly();

            try
            {
                resourceManager = new ResourceManager(resourceBaseName, resourceAssembly);
            }
            catch (Exception ex)
            {
                var msg = String.Format(CultureInfo.CurrentCulture, RuleResources.CannotCreateResourceManager, resourceBaseName, resourceAssembly);
                throw new RuleException(msg, ex);
            }
        }

        private string GetResourceString(string resourceId)
        {
            EnsureResourceManagerInitialized();
            return resourceManager.GetString(resourceId, CultureInfo.CurrentUICulture);
        }

        /// <summary>  
        /// Overrides the standard DisplayName and looks up its value inside a resources file  
        /// </summary>  
        public override string DisplayName
        {
            get
            {
                if (displayName == null)
                {
                    displayName = GetResourceString(displayNameResourceId);
                }
                return displayName;
            }
        }

        /// <summary>  
        /// Overrides the standard Description and looks up its value inside a resources file  
        /// </summary>  
        public override string Description
        {
            get
            {
                if (descriptionValue == null)
                {
                    descriptionValue = GetResourceString(descriptionResourceId);
                }
                return descriptionValue;
            }
        }
    }
}