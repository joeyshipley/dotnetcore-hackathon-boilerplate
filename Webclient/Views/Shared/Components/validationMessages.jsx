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