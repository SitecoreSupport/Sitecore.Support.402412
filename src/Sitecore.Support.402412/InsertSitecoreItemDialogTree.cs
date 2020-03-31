// -----------------------------------------------------------------------------------------------
// <copyright file="InsertSitecoreItemDialogTree.cs" company="Sitecore A/S">
//   Copyright (C) by Sitecore A/S
// </copyright>
// ----------------------------------------------------------------------------------------------

namespace Sitecore.Support.Speak.Applications
{
  using System.Net;
  using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Shell;
    using Sitecore.Speak.Applications;
    using Web;

  /// <summary>
  /// InsertSitecoreItemDialogTree class
  /// </summary>
  public class InsertSitecoreItemDialogTree : Web.PageCodes.PageCodeBase
  {
    /// <summary>
    /// Gets or sets the tree view.
    /// </summary>
    /// <value>
    /// The tree view.
    /// </value>
    public Mvc.Presentation.Rendering TreeView { get; set; }

    /// <summary>
    /// Gets or sets the list view toggle button.
    /// </summary>
    /// <value>
    /// The list view toggle button.
    /// </value>
    public Mvc.Presentation.Rendering ListViewToggleButton { get; set; }

    /// <summary>
    /// Gets or sets the tree view toggle button.
    /// </summary>
    /// <value>
    /// The tree view toggle button.
    /// </value>
    public Mvc.Presentation.Rendering TreeViewToggleButton { get; set; }

    /// <summary>
    /// Executes this instance.
    /// </summary>
    public override void Initialize()
    {
      var bucketsIsEnabled = Settings.GetSetting("BucketConfiguration.ItemBucketsEnabled");
      this.ListViewToggleButton.Parameters["IsVisible"] = bucketsIsEnabled;
      this.TreeViewToggleButton.Parameters["IsVisible"] = bucketsIsEnabled;

      this.TreeView.Parameters["ShowHiddenItems"] = UserOptions.View.ShowHiddenItems.ToString();
      this.TreeView.Parameters["ContentLanguage"] = WebUtil.GetQueryString("la");

      var selectedItemId = WebUtil.GetQueryString("id");
      if (!string.IsNullOrEmpty(selectedItemId))
      {
        var selectedItem = SelectMediaDialog.GetMediaItemFromQueryString(selectedItemId);
        this.TreeView.Parameters["PreLoadPath"] = selectedItem.Paths.LongID.Substring(1);
      }

      var ro = WebUtil.GetQueryString("ro");
      if (!string.IsNullOrEmpty(ro)) 
      {
        if (ro != "{0}")
        {
          this.TreeView.Parameters["RootItem"] = ro;
        }

        var scContent = WebUtil.GetQueryString("sc_content");
        if (!string.IsNullOrEmpty(scContent))
        {
          scContent = "&sc_content=" + WebUtility.UrlEncode(scContent);
        }

        this.ListViewToggleButton.Parameters["Click"] = string.Format(this.ListViewToggleButton.Parameters["Click"], WebUtility.UrlEncode(ro)).TrimEnd('\'') + scContent + "'";
      }
    }
  }
}