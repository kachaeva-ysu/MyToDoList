import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import actions from '../../actions/actions'
import { CommentPageWrapper, NoResultWrapper, CommentPageFooter } from '../common/styledComponents'
import Comment from './comment'
import CurrentUserComment from './currentUserComment'
import SystemComment from './systemComment'
import LoadableAddCommentPanel from './loadableAddCommentPanel'
import getRandomColor from '../../utilities/getRandomColor'
import Grid from 'material-ui/Grid'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

class CommentsPage extends Component {
  constructor(props) {
    super(props)
    this.shouldScrollDown = false
    this.userColors = {}
  }

  UNSAFE_componentWillMount() {
    const { candidate } = this.props

    this.props.setState(
      {
        pageTitle: candidate.name,
        candidateStatus: candidate.status
      }
    )
    this.userColors = {}
    Object.keys(candidate.comments).forEach((commentID) => {
      const comment = candidate.comments[commentID]
      if (!(comment.author in this.userColors)) {
        this.userColors[comment.author] = getRandomColor()
      }
    })
  }

  componentDidMount() {
    window.scrollTo(0, document.documentElement.scrollHeight)
  }

  componentDidUpdate() {
    if (this.shouldScrollDown) {
      window.scrollTo(0, document.documentElement.scrollHeight)
      this.shouldScrollDown = false
    }
  }

  handleNewCommentAdd = () => {
    this.shouldScrollDown = true
  }

  deleteComment = (commentID) => () => {
    this.props.deleteComment(this.props.candidate.id, commentID)
  }

  render() {
    const { applicationStatus, authorizationStatus, candidate, username } = this.props
    const comments = Object.keys(candidate.comments).map(commentId => candidate.comments[commentId])

    if (applicationStatus !== 'ok') {
      return <SpinnerWrapper><Spinner size={60}/></SpinnerWrapper>
    }

    if (!candidate) {
      return (
        <FormWrapper>
          <NoResultWrapper>Candidate not found</NoResultWrapper>
        </FormWrapper>
      )
    }

    let commentClouds = comments.map((comment, index) => {
      switch (comment.author) {
        case 'SYSTEM':
          return <SystemComment key={'comment-' + index} comment={comment}/>
        case username:
          return <CurrentUserComment key={'comment-' + index} comment={comment} candidate={candidate} deleteComment={this.deleteComment(comment.id)}/>
        default:
          return <Comment key={'comment-' + index} comment={comment} candidate={candidate} markerColor={this.userColors[comment.author]}/>
      }
    })

    if (comments.length === 0) {
      commentClouds = <NoResultWrapper>No comments</NoResultWrapper>
    }

    return (
      <CommentPageWrapper>
        <Grid container spacing={0} justify='center'>
          <Grid item className='comment-grid' lg={6} md={9} sm={12}>
            {commentClouds}
          </Grid>
        </Grid>
        <CommentPageFooter>
          <LoadableAddCommentPanel
            candidate={candidate}
            onCommentAdd={this.handleNewCommentAdd}
            disabled={authorizationStatus !== 'authorized'}
            username={username}
          />
        </CommentPageFooter>
      </CommentPageWrapper>
    )
  }
}

CommentsPage.propTypes = {
  candidate: PropTypes.object
}

const SpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`

export default connect(state => {
  return {
    applicationStatus: state.applicationStatus,
    authorizationStatus: state.authorizationStatus,
    username: state.username
  }
}, actions)(CommentsPage)