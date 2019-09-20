const ValidationMessages = (props) => {
  const { validation_messages } = props;
  if(!validation_messages || validation_messages.length === 0) {
    return (<div className="hide"></div>);
  }
  return (
    <div className="text-danger">
      <h4>Errors</h4>
      { validation_messages.map((message, i) => {
        return (<div key={ `validation-error-${ i }` }>{ message.text }</div>);
      })}
    </div>
  );
}
class HomeIndexPage extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div id="page-container">
        <h1 className="display-4">Welcome to the web app boilerplate!</h1>
      </div>
    );
  }
}

class PrivacyPage extends React.Component {
  render() {
    return (
      <div id="page-container">
        <p>Use this page to detail your site's privacy policy.</p>
      </div>
    );
  }
}