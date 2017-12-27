using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationShare.utilities
{
    public static class AppConstants
    {
        public const int CHANNEL_IN = 1;
        public const int CHANNEL_OUT = 2;

        public const int LIVE_AUDIO = 1;
        public const int LIVE_VIDEO = 2;

        public const int NO_DEVICE = 0;

        public const int MICROPHONE_SPEAKER_MIX = 1;
        public const int MICROPHONE = 2;
        public const int SPEAKER = 3;

        public const int MONITOR_SCREEN = 1;
        public const int WEB_CAMERA = 2;
        public const int APP_SCREEN = 3;

        public const int FRAME_WIDTH = 640;
        public const int FRAME_HEIGHT = 480;

        public const int STREAM_TYPE_NEARBY = 1;
        public const int STREAM_TYPE_COUNTRY = 2;
        public const int STREAM_TYPE_FOLLOWING = 4;

        public const int STREAM_SEARCH_TYPE_ALL = 1;
        public const int STREAM_SEARCH_TYPE_COUNTRY = 2;

        public const int STREAM_RECENT_CATEGORY_BY_ALL = 1;
        public const int STREAM_RECENT_CATEGORY_BY_PAGINATION = 2;

        public const int STREAM_BACK_TO_HOME = 1;
        public const int STREAM_BACK_TO_SEARCH = 2;
        public const int STREAM_BACK_TO_COUNTRY = 3;

        public const int STREAM_DEFAULT_SCROLL = 0;
        public const int STREAM_TOP_SCROLL = 1;
        public const int STREAM_BOTTOM_SCROLL = 2;

        public static string STREAM_GIFT_EXTENSION_PNG = ".png";
        public static string STREAM_GIFT_EXTENSION_GIF = ".gif";

        public const int STREAM_CALL_DISCONNECTED = 0;
        public const int STREAM_CALL_INCOMING = 1;
        public const int STREAM_CALL_OUTGOING = 2;
        public const int STREAM_CALL_CONNECTED = 3;
        public const int STREAM_CALL_CONNECTED_AS_VIEWER = 4;

        public const int STREAMING_FINISHED = 0;
        public const int STREAMING_CONNECTING = 1;
        public const int STREAMING_RECONNECTING = 2;
        public const int STREAMING_CONNECTED = 3;
        public const int STREAMING_POOR_NERWORK = 4;
        public const int STREAMING_INTERRUPTED = 5;
        public const int STREAMING_NO_DATA = 6;

        public const int INFO_TYPE_SHARE = 1;
        public const int INFO_TYPE_FOLLOW = 2;
        public const int INFO_TYPE_RECORD_SHARE = 3;
        public const int INFO_TYPE_LOVE = 4;
        public const int INFO_TYPE_GIFT = 5;
        public const int INFO_TYPE_MESSAGE = 6;
        public const int INFO_TYPE_BLOCK_UNBLOCK = 7;
        public const int INFO_TYPE_VOTE = 8;
        public const int INFO_TYPE_VIEWER_JOIN = 9;
        public const int INFO_TYPE_INTERRUPT = 21;
        public const int INFO_TYPE_CALL_STATE = 22;

        public const int DEFAULT_ROOM = 1;
        public const int LIKE_INFO_SENT_ON_COUNT = 100;

        public const int LIVE_SERVER_NO_ACTION = 0;
        public const int LIVE_SERVER_MENDAROTY_DOWNLOAD = 1;
        public const int LIVE_SERVER_OPTIONAL_DOWNLOAD = 2;
        public const int LIVE_SERVER_CLOSE_SESSION = 3;

        public const double STREAM_DISPLAY_MARGIN = 79.0D;

        public const int MAX_FEATURED_COUNT = 6;
        public const int MAX_CONTRIBUTION_COUNT = 10;
        public const int MAX_LIKE_COUNT = 10;
        public const int MAX_MESSAGE_COUNT = 100;
        public const int MAX_MESSAGE_LENGTH = 160;
        public const int MAX_MESSAGE_SEND_TIME = 60000;
        public const int MAX_MESSAGE_SEND_COUNT = 6;
        public const int MAX_MESSAGE_SEND_CONSEQUITIVE_FAILED_LIMIT = 3;
        public const int MAX_MESSAGE_SEND_TOTAL_FAILED_LIMIT = 5;

        public const int LIST_TOP_VIEWER = 1;
        public const int LIST_TOP_CONTRIBUTOR = 2;
        public const int LIST_TOP_DONATOR = 3;
        public const int LIST_TOP_STAR_LEVEL_VIEWER = 4;

        public const int INVITE_TYPE_CALL = 1;
        public const int INVITE_TYPE_BLOCK = 2;
        public const int INVITE_TYPE_MUTE_VIEWER = 3;
        public const int INVITE_TYPE_CALL_END = 4;
        public const int INVITE_TYPE_DISCONNECT_LIVE = 5;

        public const int GO_LIVE = 1;
        public const int TOP_STREAMS = 3;
        public const int FOLLOW_STREAMS = 9;
        public const int NEW_STREAMS = 10;
        public const int NEARBY_STREAMS = 11;

    }
}
