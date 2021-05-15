namespace OpenPr0gramm.Model
{
    public enum SyncAction
    {
        VoteItemDown = 1,
        VoteItemNeutral = 2,
        VoteItemUp = 3,
        VoteCommentDown = 4,
        VoteCommentNeutral = 5,
        VoteCommentUp = 6,
        VoteTagDown = 7,
        VoteTagNeutral = 8,
        VoteTagUp = 9,

        FollowStateFollow1 = 12,
        FollowStateNone = 13,
        FollowStateSubscribed = 14,
        FollowStateFollow2 = 15,

        CommentFav = 16,
        CommentUnFav = 17,

        ItemUnCollect = 18,
        ItemCollect = 19,
        CollectionId = 20
    }
}
