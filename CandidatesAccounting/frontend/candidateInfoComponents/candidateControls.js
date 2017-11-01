import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../materialUIDecorators/iconButton';
import RemoveIcon from 'material-ui-icons/Delete';
import EditCandidateDialog from './editCandidateDialog';
import CommentIcon from 'material-ui-icons/ViewList';
import Badge from '../materialUIDecorators/badge';
import {NavLink} from 'react-router-dom';
import AddCommentDialog from '../commentInfoComponents/addCommentDialog';

export default function CandidateControls(props) {
  return (
    <div className="float-right">
      <AddCommentDialog
        candidate={props.candidate}
        addComment={props.addComment}
        userName={props.userName}
      />

      <NavLink to={'/' + props.candidate.constructor.name.toLowerCase() + 's/' + props.candidate.id + '/comments'}>
        <Badge badgeContent={props.candidate.comments.length} badgeStyle="comment-badge">
          <IconButton
            icon={<CommentIcon />}
          />
        </Badge>
      </NavLink>

      <EditCandidateDialog
        candidate={props.candidate}
        editCandidate={props.editCandidate}
        tags={props.tags}
      />

      <IconButton
        icon={<RemoveIcon />}
        color="accent"
        onClick={() => { props.deleteCandidate(props.candidate.id) }}
      />
    </div>
  );
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  editCandidate: PropTypes.func.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  userName: PropTypes.string.isRequired,
  tags: PropTypes.object.isRequired,
};