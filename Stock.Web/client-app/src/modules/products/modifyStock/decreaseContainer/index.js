import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import PropTypes from "prop-types";
import { getProductById } from "../../list";
import { decrease } from "..";
import Form from "../../form_stock/presentation";
import { Container, Row, Col } from "reactstrap";

const Decrease = ({ initialValues, decrease: onSubmit, goBack: onCancel }) => {
  return (
    <Container fluid>
      <Row>
          <Col>
            <div className="block-header">
                <h1>Reducir stock</h1>
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

Decrease.propTypes = {
  initialValues: PropTypes.object.isRequired,
  decrease: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired
};

const mapStateToProps = (state, ownProps) => ({
  initialValues: getProductById(state, ownProps.match.params.id)
});

const mapDispatchToProps = {
  decrease,
  goBack
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Decrease);
