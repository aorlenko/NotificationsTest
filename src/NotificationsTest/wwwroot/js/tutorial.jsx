
var MessageStatus = React.createClass({
    render: function () {
        return (
        <span className='alert alert-success'>Your message id is {this.props.messageId}</span>
        )
    }
});

var MessageForm = React.createClass ({
    getInitialState: function() {
        return {
            recipients: "",
            subject: "",
            message: "",
            messageId: 0
        };
    },

    handleSubmit:function(e) {
        e.preventDefault();

        var that = this;
        
        var data = new FormData();
        data.append('recipients', this.state.recipients);
        data.append('message', this.state.message);
        data.append('subject', this.state.subject);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                alert(this.responseText);
                that.setState({ messageId: parseInt(this.responseText) });
            }
        };
        xhr.send(data);

        this.setState({
            recipients: "",
            subject: "",
            message: ""
        });
    },

    handleRecipientsChange:function(e) {
        this.setState({recipients: event.target.value});
    },

    handleSubjectChange: function (e) {
        this.setState({ subject: event.target.value });
    },

    handleMessageChange: function (e) {
        this.setState({ message: event.target.value });
    },

    render: function() {
        return (
                <form onSubmit={this.handleSubmit}>
                  <div className="form-group">
                    <label htmlFor="recipients">Recipients</label>
                    <input type="text" value={this.state.recipients} onChange={this.handleRecipientsChange} className="form-control" id="recipients" placeholder="Recipients" required />
                  </div>
                  <div className="form-group">
                    <label htmlFor="subject">Subject</label>
                    <input type="text" value={this.state.subject} onChange={this.handleSubjectChange} className="form-control" id="subject" placeholder="Subject" required />
                  </div>
                  <div className="form-group">
                    <label htmlFor="message">Message</label>
                    <input type="text" value={this.state.message} onChange={this.handleMessageChange} className="form-control" id="message" placeholder="Message" required />
                  </div>
                  <button type="submit" className="btn btn-default">Submit</button>&nbsp;
                  { this.state.messageId > 0 ? <MessageStatus messageId={this.state.messageId}/> : null }
                </form>
            )
    }
});

class Test extends React.Component {
    constructor(props) {
        super(props);
        this.state={name:'andrey'}
    }

    render() {
        return (
            <div>Test {this.state.name}</div>
            )
    }
}

var Comment = React.createClass({
  rawMarkup: function() {
    var md = new Remarkable();
    var rawMarkup = md.render(this.props.children.toString());
    return { __html: rawMarkup };
  },

  render: function() {
    return (
      <div className="comment">
        <h2 className="commentAuthor">{this.props.author}
        </h2>
        <span dangerouslySetInnerHTML={this.rawMarkup()} />
      </div>
    );
  }
});

var CommentList = React.createClass({
    render: function() {
        var commentNodes = this.props.data.map(function(comment) {
            return (
              <Comment author={comment.author} key={comment.id}>
                {comment.text}
              </Comment>
              );
            });

            return (
              <div className="commentList">
                {commentNodes}
              </div>
                );
            }
});

var CommentForm = React.createClass({
  getInitialState: function() {
    return {author: '', text: ''};
  },
  handleAuthorChange: function(e) {
    this.setState({author: e.target.value});
  },
  handleTextChange: function(e) {
    this.setState({text: e.target.value});
  },
  handleSubmit: function(e) {
    e.preventDefault();
    var author = this.state.author.trim();
    var text = this.state.text.trim();
    if (!text || !author) {
      return;
    }
    this.props.onCommentSubmit({author: author, text: text});
    this.setState({author: '', text: ''});
  },
  render: function() {
    return (
      <form className="commentForm" onSubmit={this.handleSubmit}>
        <input type="text"
               placeholder="Your name"
               value={this.state.author}
               onChange={this.handleAuthorChange} />
        <input type="text"
               placeholder="Say something..."
               value={this.state.text}
               onChange={this.handleTextChange} />
        <input type="submit" value="Post" />
      </form>
    );
  }
});

var CommentBox = React.createClass({
  loadCommentsFromServer: function() {
    var xhr = new XMLHttpRequest();
    xhr.open('get', this.props.url, true);
    xhr.onload = function() {
      var data = JSON.parse(xhr.responseText);
      this.setState({ data: data });
    }.bind(this);
    xhr.send();
  },
  handleCommentSubmit: function(comment) {
    var data = new FormData();
    data.append('author', comment.author);
    data.append('text', comment.text);

    var xhr = new XMLHttpRequest();
    xhr.open('post', this.props.submitUrl, true);
    xhr.onload = function() {
      this.loadCommentsFromServer();
    }.bind(this);
    xhr.send(data);
  },
  getInitialState: function() {
    return {data: []};
  },
  componentDidMount: function() {
    this.loadCommentsFromServer();
    //window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
  },
  render: function() {
    return (
      <div className="commentBox">
          <Test/>
        <h1>Comments</h1>
        <CommentList data={this.state.data} />
        <CommentForm onCommentSubmit={this.handleCommentSubmit} />
      </div>
    );
  }
});

ReactDOM.render(
  //<CommentBox url="/comments" submitUrl="/comments/register" pollInterval={2000} />,
  <MessageForm submitUrl="/messages/send" />,
  document.getElementById('content')
);