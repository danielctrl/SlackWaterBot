using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Lib.Resources
{
    public class SlackJSONProperties
    {
        public static readonly string Url = "url";
        public static readonly string Type = "type";
        public static readonly string Subtype = "subtype";
        public static readonly string Username = "username";
        public static readonly string Fallback = "fallback";
    }

    public class SlackUserJSONProps
    {
        public static readonly string Id = "id";
        public static readonly string Name = "name";
        public static readonly string Deleted = "deleted";
        public static readonly string Profile = "profile";
        public static readonly string IsAdmin = "is_admin";
        public static readonly string IsOwner = "is_owner";
    }

    public class SlackUserProfileJSONProps
    {
        public static readonly string FirstName = "first_name";
        public static readonly string LastName = "last_name";
        public static readonly string RealName = "real_name";
        public static readonly string Email = "email";
        public static readonly string Skype = "skype";
        public static readonly string Phone = "phone";
    }

    public class SlackChannelJSONProps
    {
        public static readonly string Id = "id";
        public static readonly string Name = "name";
        public static readonly string IsChannel = "is_channel";
        public static readonly string Creator = "creator";
        public static readonly string IsArchived = "is_archived";
        public static readonly string IsGeneral = "is_general";
        public static readonly string IsMember = "is_member";
        public static readonly string Members = "members";
        public static readonly string Topic = "topic";
        public static readonly string Purpose = "purpose";
    }

    public class SlackChannelTopicPurposeJSONProps
    {
        public static readonly string Value = "value";
        public static readonly string Creator = "Creator";
    }

    public class SlackMessageJSONProps
    {
        public static readonly string Text = "text";
        public static readonly string TimeStamp = "ts";
        public static readonly string Channel = "channel";
        public static readonly string User = "user";
        public static readonly string Team = "team";
        public static readonly string BotId = "bot_id";
        public static readonly string Attachments = "attachments";
    }

    /// <summary>
    /// Reference: https://api.slack.com/methods
    /// </summary>
    public class SlackAPICalls
    {
        /// <summary>
        /// Checks API calling code.
        /// </summary>
        public static readonly string APITest = "api.test";

        /// <summary>
        /// Revokes a token.
        /// </summary>
        public static readonly string AuthRevoke = "auth.revoke";

        /// <summary>
        /// Checks authentication & identity.
        /// </summary>
        public static readonly string AuthTest = "auth.test";

        /// <summary>
        /// Gets information about a bot user.
        /// </summary>
        public static readonly string BotsInfo = "bots.info";

        /// <summary>
        /// Archives a channel.
        /// </summary>
        public static readonly string ChannelsArchive = "channels.archive";

        /// <summary>
        /// Creates a channel.
        /// </summary>
        public static readonly string ChannelsCreate = "channels.create";

        /// <summary>
        /// Fetches history of messages and events from a channel.
        /// </summary>
        public static readonly string ChannelsHistory = "channels.history";

        /// <summary>
        /// Gets information about a channel.
        /// </summary>
        public static readonly string ChannelsInfo = "channels.info";

        /// <summary>
        /// Invites a user to a channel.
        /// </summary>
        public static readonly string ChannelsInvite = "channels.invite";

        /// <summary>
        /// Joins a channel, creating it if needed.
        /// </summary>
        public static readonly string ChannelsJoin = "channels.join";


        /// <summary>
        /// Removes a user from a channel.
        /// </summary>
        public static readonly string ChannelsKick = "channels.kick";

        /// <summary>
        /// Leaves a channel.
        /// </summary>
        public static readonly string ChannelsLeave = "channels.leave";

        /// <summary>
        /// Lists all channels in a Slack team.
        /// </summary>
        public static readonly string ChannelsList = "channels.list";

        /// <summary>
        /// Sets the read cursor in a channel.
        /// </summary>
        public static readonly string ChannelsMark = "channels.mark";

        /// <summary>
        /// Renames a channel.
        /// </summary>
        public static readonly string ChannelsRename = "channels.rename";

        /// <summary>
        /// Sets the purpose for a channel.
        /// </summary>
        public static readonly string ChannelsSetPurpose = "channels.setPurpose";

        /// <summary>
        /// Sets the topic for a channel.
        /// </summary>
        public static readonly string ChannelsSetTopic = "channels.setTopic";
        
        /// <summary>
        /// Unarchives a channel.
        /// </summary>
        public static readonly string ChannelsUnarchive = "channels.unarchive";

        /// <summary>
        /// Deletes a message.
        /// </summary>
        public static readonly string ChatDelete = "chat.delete";

        /// <summary>
        /// Share a me message into a channel.
        /// </summary>
        public static readonly string ChatMeMessage = "chat.meMessage";

        /// <summary>
        /// Sends a message to a channel.
        /// </summary>
        public static readonly string ChatPostMessage = "chat.postMessage";

        /// <summary>
        /// Updates a message.
        /// </summary>
        public static readonly string ChatUpdate = "chat.update";

        /// <summary>
        /// Ends the current user's Do Not Disturb session immediately.
        /// </summary>
        public static readonly string DoNotDisturbEndDnD = "dnd.endDnd";

        /// <summary>
        /// Ends the current user's snooze mode immediately.
        /// </summary>
        public static readonly string DoNotDisturbEndSnooze = "dnd.endSnooze";

        /// <summary>
        /// Retrieves a user's current Do Not Disturb status.
        /// </summary>
        public static readonly string DoNotDisturbInfo = "dnd.info";

        /// <summary>
        /// Turns on Do Not Disturb mode for the current user, or changes its duration.
        /// </summary>
        public static readonly string DoNotDisturbSetSnooze = "dnd.setSnooze";

        /// <summary>
        /// Retrieves the Do Not Disturb status for users on a team.
        /// </summary>
        public static readonly string DoNotDisturbTeamInfo = "dnd.teamInfo";

        /// <summary>
        /// Lists custom emoji for a team.
        /// </summary>
        public static readonly string EmojiList = "emoji.list";

        /// <summary>
        /// Add a comment to an existing file.
        /// </summary>
        public static readonly string FilesCommentsAdd = "files.comments.add";

        /// <summary>
        /// Deletes an existing comment on a file.
        /// </summary>
        public static readonly string FilesCommentsDelete = "files.comments.delete";

        /// <summary>
        /// Edit an existing file comment.
        /// </summary>
        public static readonly string FilesCommentsEdit = "files.comments.edit";

        /// <summary>
        /// Deletes a file.
        /// </summary>
        public static readonly string FilesDelete = "files.delete";

        /// <summary>
        /// Gets information about a team file.
        /// </summary>
        public static readonly string FilesInfo = "files.info";

        /// <summary>
        /// Lists & filters team files.
        /// </summary>
        public static readonly string FilesList = "files.list";

        /// <summary>
        /// Revokes public/external sharing access for a file
        /// </summary>
        public static readonly string FilesRevokePublicURL = "files.revokePublicURL";

        /// <summary>
        /// Enables a file for public/external sharing.
        /// </summary>
        public static readonly string FilesSharedPublicURL = "files.sharedPublicURL";

        /// <summary>
        /// Uploads or creates a file.
        /// </summary>
        public static readonly string FilesUpload = "files.upload";

        /// <summary>
        /// Archives a private channel.
        /// </summary>
        public static readonly string GroupsArchive = "groups.archive";

        /// <summary>
        /// Closes a private channel.
        /// </summary>
        public static readonly string GroupsClose = "groups.close";

        /// <summary>
        /// Creates a private channel.
        /// </summary>
        public static readonly string GroupsCreate = "groups.create";

        /// <summary>
        /// Clones and archives a private channel.
        /// </summary>
        public static readonly string GroupsCreateChild = "groups.createChild";

        /// <summary>
        /// Fetches history of messages and events from a private channel.
        /// </summary>
        public static readonly string GroupsHistory = "groups.history";

        /// <summary>
        /// Gets information about a private channel.
        /// </summary>
        public static readonly string GroupsInfo = "groups.info";

        /// <summary>
        /// Invites a user to a private channel.
        /// </summary>
        public static readonly string GroupsInvite = "groups.invite";

        /// <summary>
        /// Removes a user from a private channel.
        /// </summary>
        public static readonly string GroupsKick = "groups.kick";

        /// <summary>
        /// Leaves a private channel.
        /// </summary>
        public static readonly string GroupsLeave = "groups.leave";

        /// <summary>
        /// Lists private channels that the calling user has access to.
        /// </summary>
        public static readonly string GroupsList = "groups.list";

        /// <summary>
        /// Sets the read cursor in a private channel.
        /// </summary>
        public static readonly string GroupsMark = "groups.mark";

        /// <summary>
        /// Sets the read cursor in a private channel.Opens a private channel.
        /// </summary>
        public static readonly string GroupsOpen = "groups.open";

        /// <summary>
        /// Renames a private channel.
        /// </summary>
        public static readonly string GroupsRename = "groups.rename";

        /// <summary>
        /// Sets the purpose for a private channel.
        /// </summary>
        public static readonly string GroupsSetPurpose = "groups.setPurpose";

        /// <summary>
        /// Sets the topic for a private channel.
        /// </summary>
        public static readonly string GroupsSetTopic = "groups.setTopic";

        /// <summary>
        /// Unarchives a private channel.
        /// </summary>
        public static readonly string GroupsUnarchive = "groups.unarchive";

        /// <summary>
        /// Close a direct message channel.
        /// </summary>
        public static readonly string IMClose = "im.close";

        /// <summary>
        /// Fetches history of messages and events from direct message channel.
        /// </summary>
        public static readonly string IMHistory = "im.history";

        /// <summary>
        /// Lists direct message channels for the calling user.
        /// </summary>
        public static readonly string IMList = "im.list";

        /// <summary>
        /// Sets the read cursor in a direct message channel.
        /// </summary>
        public static readonly string IMMark = "im.mark";

        /// <summary>
        /// Opens a direct message channel.
        /// </summary>
        public static readonly string IMOpen = "im.open";

        /// <summary>
        /// Closes a multiparty direct message channel.
        /// </summary>
        public static readonly string MpIMClose = "mpim.close";

        /// <summary>
        /// Fetches history of messages and events from a multiparty direct message.
        /// </summary>
        public static readonly string MpIMHistory = "mpim.history";

        /// <summary>
        /// Lists multiparty direct message channels for the calling user.
        /// </summary>
        public static readonly string MpIMList = "mpim.list";

        /// <summary>
        /// Sets the read cursor in a multiparty direct message channel
        /// </summary>
        public static readonly string MpIMMark = "mpim.mark";

        /// <summary>
        /// This method opens a multiparty direct message.
        /// </summary>
        public static readonly string MpIMOpen = "mpim.open";

        /// <summary>
        /// Exchanges a temporary OAuth code for an API token.
        /// </summary>
        public static readonly string OauthAccess = "oauth.access";

        /// <summary>
        /// Pins an item to a channel.
        /// </summary>
        public static readonly string PinsAdd = "pins.add";

        /// <summary>
        /// Lists items pinned to a channel.
        /// </summary>
        public static readonly string PinsList = "pins.list";

        /// <summary>
        /// Un-pins an item from a channel.
        /// </summary>
        public static readonly string PinsRemove = "pins.remove";

        /// <summary>
        /// Adds a reaction to an item.
        /// </summary>
        public static readonly string ReactionsAdd = "reactions.add";

        /// <summary>
        /// Gets reactions for an item.
        /// </summary>
        public static readonly string ReactionsGet = "reactions.get";

        /// <summary>
        /// Lists reactions made by a user.
        /// </summary>
        public static readonly string ReactionsList = "reactions.list";

        /// <summary>
        /// Removes a reaction from an item.
        /// </summary>
        public static readonly string ReactionsRemove = "reactions.remove";

        /// <summary>
        /// Creates a reminder.
        /// </summary>
        public static readonly string RemindersAdd = "reminders.add";

        /// <summary>
        /// Marks a reminder as complete.
        /// </summary>
        public static readonly string RemindersComplete = "reminders.complete";

        /// <summary>
        /// Deletes a reminder.
        /// </summary>
        public static readonly string RemindersDelete = "reminders.delete";

        /// <summary>
        /// Gets information about a reminder.
        /// </summary>
        public static readonly string RemindersInfo = "reminders.info";

        /// <summary>
        /// Lists all reminders created by or for a given user.
        /// </summary>
        public static readonly string RemindersList = "reminders.list";

        /// <summary>
        /// Starts a Real Time Messaging session.
        /// </summary>
        public static readonly string RTMStart = "rtm.start";

        /// <summary>
        /// Searches for messages and files matching a query.
        /// </summary>
        public static readonly string SearchAll = "search.all";

        /// <summary>
        /// Searches for files matching a query.
        /// </summary>
        public static readonly string SearchFiles = "search.files";

        /// <summary>
        /// Searches for messages matching a query.
        /// </summary>
        public static readonly string SearchMessages = "search.messages";

        /// <summary>
        /// Adds a star to an item.
        /// </summary>
        public static readonly string StarsAdd = "stars.add";

        /// <summary>
        /// Lists stars for a user.
        /// </summary>
        public static readonly string StarsList = "stars.list";

        /// <summary>
        /// Removes a star from an item.
        /// </summary>
        public static readonly string StarsRemove = "stars.remove";

        /// <summary>
        /// Gets the access logs for the current team.
        /// </summary>
        public static readonly string TeamAcessLogs = "team.accessLogs";

        /// <summary>
        /// Gets billable users information for the current team.
        /// </summary>
        public static readonly string TeamBillableInfo = "team.billableInfo";

        /// <summary>
        /// Gets information about the current team.
        /// </summary>
        public static readonly string TeamInfo = "team.info";

        /// <summary>
        /// Gets the integration logs for the current team.
        /// </summary>
        public static readonly string TeamIntegrationLogs = "team.integrationLogs";

        /// <summary>
        /// Retrieve a team's profile.
        /// </summary>
        public static readonly string TeamProfileGet = "team.profile.get";

        /// <summary>
        /// Create a User Group
        /// </summary>
        public static readonly string UserGroupsCreate = "usergroups.create";

        /// <summary>
        /// Disable an existing User Group
        /// </summary>
        public static readonly string UserGroupsDisable = "usergroups.disable";

        /// <summary>
        /// Enable a User Group
        /// </summary>
        public static readonly string UserGroupsEnable = "usergroups.enable";

        /// <summary>
        /// List all User Groups for a team
        /// </summary>
        public static readonly string UserGroupsList = "usergroups.list";

        /// <summary>
        /// Update an existing User Group
        /// </summary>
        public static readonly string UserGroupsUpdate = "usergroups.update";

        /// <summary>
        /// List all users in a User Group
        /// </summary>
        public static readonly string UserGroupsUsersList = "usergroups.users.list";

        /// <summary>
        /// Update the list of users for a User Group
        /// </summary>
        public static readonly string UserGroupsUsersUpdate = "usergroups.users.update";

        /// <summary>
        /// Delete the user profile photo
        /// </summary>
        public static readonly string UsersDeletePhoto = "users.deletePhoto";

        /// <summary>
        /// Gets user presence information.
        /// </summary>
        public static readonly string UsersGetPresence = "users.getPresence";

        /// <summary>
        /// Get a user's identity.
        /// </summary>
        public static readonly string UsersIdentity = "users.identity";

        /// <summary>
        /// Gets information about a user.
        /// </summary>
        public static readonly string UsersInfo = "users.info";

        /// <summary>
        /// Lists all users in a Slack team.
        /// </summary>
        public static readonly string UsersList = "users.list";

        /// <summary>
        /// Marks a user as active.
        /// </summary>
        public static readonly string UsersSetActive = "users.setActive";

        /// <summary>
        /// Set the user profile photo
        /// </summary>
        public static readonly string UsersSetPhoto = "users.setPhoto";

        /// <summary>
        /// Manually sets user presence.
        /// </summary>
        public static readonly string UsersSetPresence = "users.setPresence";

        /// <summary>
        /// Retrieves a user's profile information.
        /// </summary>
        public static readonly string UsersProfileGet = "users.profile.get";

        /// <summary>
        /// Set the profile information for a user.
        /// </summary>
        public static readonly string UsersProfileSet = "users.profile.set";
    }

    /// <summary>
    /// Reference: https://api.slack.com/events (RTM Events)
    /// </summary>
    public class SlackRTMEventTypes
    {
        /// <summary>
        /// The list of accounts a user is signed into has changed
        /// </summary>
        public static readonly string AccountsChanged = "accounts_changed";

        /// <summary>
        /// An bot user was added
        /// </summary>
        public static readonly string BotAdded = "bot_added";

        /// <summary>
        /// An bot user was changed
        /// </summary>
        public static readonly string BotChanged = "bot_changed";

        /// <summary>
        /// A channel was archived
        /// </summary>
        public static readonly string ChannelArchive = "channel_archive";

        /// <summary>
        /// A channel was created
        /// </summary>
        public static readonly string ChannelCreated = "channel_created";

        /// <summary>
        /// A channel was deleted
        /// </summary>
        public static readonly string ChannelDeleted = "channel_deleted";

        /// <summary>
        /// Bulk updates were made to a channel's history
        /// </summary>
        public static readonly string ChannelHistoryChanged = "channel_history_changed";

        /// <summary>
        /// You joined a channel
        /// </summary>
        public static readonly string ChannelJoined = "channel_joined";

        /// <summary>
        /// You left a channel
        /// </summary>
        public static readonly string ChannelLeft = "channel_left";

        /// <summary>
        /// Your channel read marker was updated
        /// </summary>
        public static readonly string ChannelMarked = "channel_marked";

        /// <summary>
        /// A channel was renamed
        /// </summary>
        public static readonly string ChannelRename = "channel_rename";

        /// <summary>
        /// A channel was unarchived
        /// </summary>
        public static readonly string ChannelUnarchive = "channel_unarchive";

        /// <summary>
        /// A team slash command has been added or changed
        /// </summary>
        public static readonly string CommandsChanged = "commands_changed";

        /// <summary>
        /// Do not Disturb settings changed for the current user
        /// </summary>
        public static readonly string DoNotDisturbUpdated = "dnd_updated";

        /// <summary>
        /// Do not Disturb settings changed for a team member
        /// </summary>
        public static readonly string DoNotDisturbUpdatedUser = "dnd_updated_user";

        /// <summary>
        /// The team email domain has changed
        /// </summary>
        public static readonly string EmailDomainChanged = "email_domain_changed";

        /// <summary>
        /// A team custom emoji has been added or changed
        /// </summary>
        public static readonly string EmojiChanged = "emoji_changed";

        /// <summary>
        /// A file was changed
        /// </summary>
        public static readonly string FileChange = "file_change";

        /// <summary>
        /// A file comment was added
        /// </summary>
        public static readonly string FileCommentAdded = "file_comment_added";

        /// <summary>
        /// A file comment was deleted
        /// </summary>
        public static readonly string FileCommentDeleted = "file_comment_deleted";

        /// <summary>
        /// A file comment was edited
        /// </summary>
        public static readonly string FileCommentEdited = "file_comment_edited";

        /// <summary>
        /// A file was created
        /// </summary>
        public static readonly string FileCreated = "file_created";

        /// <summary>
        /// A file was deleted
        /// </summary>
        public static readonly string FileDeleted = "file_deleted";

        /// <summary>
        /// A file was made public
        /// </summary>
        public static readonly string FilePublic = "file_public";

        /// <summary>
        /// A file was shared
        /// </summary>
        public static readonly string FileShared = "file_shared";

        /// <summary>
        /// A file was unshared
        /// </summary>
        public static readonly string FileUnshared = "file_unshared";

        /// <summary>
        /// A private channel was archived
        /// </summary>
        public static readonly string GroupArchive = "group_archive";

        /// <summary>
        /// You closed a private channel
        /// </summary>
        public static readonly string GroupClose = "group_close";

        /// <summary>
        /// Bulk updates were made to a private channel's history
        /// </summary>
        public static readonly string GroupHistoryChanged = "group_history_changed";

        /// <summary>
        /// You joined a private channel
        /// </summary>
        public static readonly string GroupJoined = "group_joined";

        /// <summary>
        /// You left a private channel
        /// </summary>
        public static readonly string GroupLeft = "group_left";

        /// <summary>
        /// A private channel read marker was updated
        /// </summary>
        public static readonly string GroupMarked = "group_marked";

        /// <summary>
        /// You opened a private channel
        /// </summary>
        public static readonly string GroupOpen = "group_open";

        /// <summary>
        /// A private channel was renamed
        /// </summary>
        public static readonly string GroupRename = "group_rename";

        /// <summary>
        /// A private channel was unarchived
        /// </summary>
        public static readonly string GroupUnarchive = "group_unarchive";

        /// <summary>
        /// The client has successfully connected to the server
        /// </summary>
        public static readonly string Hello = "hello";

        /// <summary>
        /// You closed a DM
        /// </summary>
        public static readonly string IMClose = "im_close";

        /// <summary>
        /// A DM was created
        /// </summary>
        public static readonly string IMCreated = "im_created";

        /// <summary>
        /// Bulk updates were made to a DM's history
        /// </summary>
        public static readonly string IMHistoryChanged = "im_history_changed";

        /// <summary>
        /// A direct message read marker was updated
        /// </summary>
        public static readonly string IMMarked = "im_marked";

        /// <summary>
        /// You opened a DM
        /// </summary>
        public static readonly string IMOpen = "im_open";

        /// <summary>
        /// You manually updated your presence
        /// </summary>
        public static readonly string ManualPresenceChange = "manual_presence_change";

        /// <summary>
        /// A message was sent to a channel
        /// </summary>
        public static readonly string Message = "message";

        /// <summary>
        /// A pin was added to a channel
        /// </summary>
        public static readonly string PinAdded = "pin_added";

        /// <summary>
        /// A pin was removed from a channel
        /// </summary>
        public static readonly string PinRemoved = "pin_removed";

        /// <summary>
        /// You have updated your preferences
        /// </summary>
        public static readonly string PrefChange = "pref_change";

        /// <summary>
        /// A team member's presence changed
        /// </summary>
        public static readonly string PresenceChange = "presence_change";

        /// <summary>
        /// A team member has added an emoji reaction to an item
        /// </summary>
        public static readonly string ReactionAdded = "reaction_added";

        /// <summary>
        /// A team member removed an emoji reaction
        /// </summary>
        public static readonly string ReactionRemoved = "reaction_removed";

        /// <summary>
        /// Experimental
        /// </summary>
        public static readonly string ReconnectUrl = "reconnect_url";

        /// <summary>
        /// A team member has starred an item
        /// </summary>
        public static readonly string StarAdded = "star_added";

        /// <summary>
        /// A team member removed a star
        /// </summary>
        public static readonly string StarRemoved = "star_removed";

        /// <summary>
        /// A User Group has been added to the team
        /// </summary>
        public static readonly string SubteamCreated = "subteam_created";

        /// <summary>
        /// You have been added to a User Group
        /// </summary>
        public static readonly string SubteamSelfAdded = "subteam_self_added";

        /// <summary>
        /// You have been removed from a User Group
        /// </summary>
        public static readonly string SubteamSelfRemoved = "subteam_self_removed";

        /// <summary>
        /// An existing User Group has been updated or its members changed
        /// </summary>
        public static readonly string SubteamUpdated = "subteam_updated";

        /// <summary>
        /// The team domain has changed
        /// </summary>
        public static readonly string TeamDomainChange = "team_domain_change";

        /// <summary>
        /// A new team member has joined
        /// </summary>
        public static readonly string TeamJoin = "team_join";

        /// <summary>
        /// The team is being migrated between servers
        /// </summary>
        public static readonly string TeamMigrationStarted = "team_migration_started";

        /// <summary>
        /// The team billing plan has changed
        /// </summary>
        public static readonly string TeamPlanChange = "team_plan_change";

        /// <summary>
        /// A team preference has been updated
        /// </summary>
        public static readonly string TeamPrefChange = "team_pref_change";

        /// <summary>
        /// Team profile fields have been updated
        /// </summary>
        public static readonly string TeamProfileChange = "team_profile_change";

        /// <summary>
        /// Team profile fields have been deleted
        /// </summary>
        public static readonly string TeamProfileDelete = "team_profile_delete";

        /// <summary>
        /// Team profile fields have been reordered
        /// </summary>
        public static readonly string TeamProfileReorder = "team_profile_reorder";

        /// <summary>
        /// The team name has changed
        /// </summary>
        public static readonly string TeamRename = "team_rename";

        /// <summary>
        /// A team member's data has changed
        /// </summary>
        public static readonly string UserChange = "user_change";

        /// <summary>
        /// A channel member is typing a message
        /// </summary>
        public static readonly string UserTyping = "user_typing";
    }
}
