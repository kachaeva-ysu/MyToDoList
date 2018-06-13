import 'react-quill/dist/quill.snow.css'
import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { BigButtonStyle, SmallIconStyle, QuillToolbarButtonStyle } from '../../commonComponents/styleObjects'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import AttachIcon from '@material-ui/icons/AttachFile'
import AddIcon from '@material-ui/icons/AddCircleOutline'
import ReactQuill from 'react-quill'
import Comment from '../../utilities/comment'
import SubscribeButton from './subscribeButton'
import FileUploader from '../../commonComponents/fileUploader'
import SetCursorToEnd from '../../utilities/setCursorToEnd'
import styled from 'styled-components'

export default class AddCommentPanel extends Component {
  constructor(props) {
    super(props);
    this.state = ({ commentText: '', commentAttachment: null })
  }

  handleChange = (commentText) => {
    this.setState({commentText})
  }

  handleAttachFile = (commentAttachment) => {
    this.setState({ commentAttachment })
  }

  handleEnterPress = (event) => {
    if (event.keyCode === 13) {
      if (!event.shiftKey) {
        event.preventDefault()
        this.handleCommentAdd()
      }
    }
  }

  handleCommentAdd = () => {
    const {addComment, username, onCommentAdd, candidate, disabled } = this.props

    if (disabled) {
      return
    }

    let commentText = this.state.commentText
    if (commentText.replace(/<[^>]+>/g,'').trim() !== '' || this.state.commentAttachment) {
      if (commentText.slice(-11) === '<p><br></p>') {
        commentText = commentText.substr(0, commentText.length - 11)
      }
      addComment({
        candidateId: candidate.id,
        comment: new Comment(
          username,
          commentText,
          this.state.commentAttachment ? this.state.commentAttachment.name : ''
        ),
        commentAttachment: this.state.commentAttachment
      })
      onCommentAdd()
      this.setState({ commentText: '', commentAttachment: null })
    }
  }

  customQuillToolbar = () => {
    const { disabled } = this.props
    return (
      <div id='toolbar'>
        <div className={'flex centered' + (disabled ? ' disabled' : '')}>
          {
            /*
            <select defaultValue='' className='ql-size' disabled={disabled}>
              <option value='small'/>
              <option value=''/>
              <option value='large'/>
              <option value='huge'/>
            </select>
            */
          }
          <button className='ql-bold' disabled={disabled}/>
          <button className='ql-italic' disabled={disabled}/>
          <button className='ql-underline' disabled={disabled}/>
          <button className='ql-strike' disabled={disabled}/>
          <button className='ql-list' value='ordered' disabled={disabled}/>
          <button className='ql-list' value='bullet' disabled={disabled}/>
          <button className='ql-indent' value='-1' disabled={disabled}/>
          <button className='ql-indent' value='+1' disabled={disabled}/>
          <button className='ql-link' disabled={disabled}/>
          <button className='quill-custom-button' onClick={() => { this.addCommentPattern('&#x039F(n)') }} disabled={disabled}>Ο(n)</button>
          <button className='quill-custom-button' onClick={() => { this.addCommentPattern('&#x039F(nlogn)') }} disabled={disabled}>Ο(nlogn)</button>
          <button className='quill-custom-button' onClick={() => { this.addCommentPattern('&#x039F(n^2)') }} disabled={disabled}>Ο(n^2)</button>
          <FileUploader
            uploadFile={this.handleAttachFile}
            fileTypes={['pdf', 'doc', 'docx', 'txt', 'png', 'jpg', 'jpeg', 'gif', 'ico', 'bmp']}
            icon={<AttachIcon style={QuillToolbarButtonStyle}/>}
            attachment={this.state.commentAttachment}
            disabled={disabled} />
        </div>
      </div>
    )
  }

  addCommentPattern = (patternValue) => {
    const commentTextDiv = document.getElementById('commentReactQuill').firstElementChild.firstElementChild
    let innerHTML = commentTextDiv.innerHTML
    let i = innerHTML.length - 1
    for (; i >= 4; i--)
    {
      if (innerHTML[i] === '<' && (innerHTML[i - 1] !== '>' || innerHTML.substring(i - 4, i) === '<br>')) {
        break
      }
    }
    commentTextDiv.innerHTML = innerHTML.substring(0, i) + patternValue + innerHTML.substring(i, innerHTML.length)
    SetCursorToEnd(commentTextDiv)
  }

  render() {
    const { disabled, candidate, username, subscribe, unsubscribe } = this.props

    return (
      <AddCommentPanelWrapper>
        <QuillWrapper>
          { this.customQuillToolbar() }
          <ReactQuill
            id='commentReactQuill'
            value={this.state.commentText}
            onChange={this.handleChange}
            placeholder='New comment'
            tabIndex={1}
            modules={AddCommentPanel.modules}
            onKeyDown={this.handleEnterPress}
          />
        </QuillWrapper>
        <AddCommentPanelButtonsWrapper>
          <SubscribeButtonWrapper>
            <SubscribeButton
              active={!disabled && !!candidate.subscribers[username]}
              subscribe={() => { subscribe({ candidateId: candidate.id })}}
              unsubscribe={() => { unsubscribe({ candidateId: candidate.id })}}
              disabled={disabled}
            />
          </SubscribeButtonWrapper>
          <AddCommentButtonWrapper>
            <IconButton
              icon={<AddIcon style={SmallIconStyle}/>}
              disabled={disabled}
              onClick={this.handleCommentAdd}
              style={BigButtonStyle}
            />
          </AddCommentButtonWrapper>
        </AddCommentPanelButtonsWrapper>
      </AddCommentPanelWrapper>
    )
  }
}

AddCommentPanel.propTypes = {
  username: PropTypes.string.isRequired,
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  onCommentAdd: PropTypes.func,
  disabled: PropTypes.bool,
}

AddCommentPanel.modules = {
  toolbar: {
    container: '#toolbar',
  }
}

const AddCommentPanelButtonsWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  align-items: center;
  position: absolute;
  height: 100%;
  right: 2px;
  bottom: 2px;
 `

const AddCommentPanelWrapper = styled.div`
  position: relative;
  box-sizing: border-box;
  width: 100%;
  clear: both;
  border-right: 1px solid rgba(0, 0, 0, 0.2);
  background: #FFF;
`

const AddCommentButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  bottom: 0px;
  right: 5px;
`

const QuillWrapper = styled.div`
  padding-right: 74px;
`

const SubscribeButtonWrapper = styled.div`
  display: inline-block;
  position: relative;
  top: 11px;
  right: 11px;
`