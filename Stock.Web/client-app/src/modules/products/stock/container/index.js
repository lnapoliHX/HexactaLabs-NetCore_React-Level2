import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import PropTypes from "prop-types";
import { stock } from "..";
import Form from "../presentation";
import { Container, Row, Col } from "reactstrap";

const Stock = ({ initialValues, stock: onSubmit, goBack: onCancel }) => {
  return (
    <Container fluid>
      <Row>
          <Col>
            <div className="block-header">
                <h1>{initialValues.increase ? 'Incrementar' : 'Decrementar'} Stock</h1>
            </div>
        </Col>
      </Row>
      <Row>
        <Col>
          <Form
            initialValues={initialValues}
            onSubmit={onSubmit}
            handleCancel={onCancel}
          />
        </Col>
      </Row>
    </Container>
  );
};

Stock.propTypes = {
  initialValues: PropTypes.object.isRequired,
  stock: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired
};

const mapStateToProps = (state, ownProps) => ({
  initialValues: {
      increase : ownProps.match.params.action === 'increase',
      id: ownProps.match.params.id
  }
});

const mapDispatchToProps = {
  stock,
  goBack
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Stock);